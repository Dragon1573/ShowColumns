using System;
using System.Globalization;

namespace ShowColumns.Commands
{
    internal class ColumnItem : IEquatable<ColumnItem>
    {
        public object Group { get; }
        public string Name { get; }
        public TextStyle Style { get; set; }
        public int Width => GetDisplayWidth(Name);

        private static int GetDisplayWidth(string s)
        {
            int width = 0;
            foreach (var c in s)
                width += CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.OtherLetter
                    || (c >= '\u1100' && c <= '\u9FFF')
                    || (c >= '\uAC00' && c <= '\uD7AF')
                    || (c >= '\uF900' && c <= '\uFAFF')
                    || (c >= '\uFE10' && c <= '\uFE6F')
                    || (c >= '\uFF00' && c <= '\uFFEF')
                    ? 2 : 1;
            return width;
        }

        public ColumnItem(object group, string name, TextStyle style)
        {
            this.Group = group;
            this.Name = name ?? string.Empty;
            this.Style = style;
        }

        public bool Equals(ColumnItem other)
            => object.Equals(Group, other.Group)
                && object.Equals(Name, other.Name);

        public override bool Equals(object obj)
            => obj is ColumnItem other
                && this.Equals(other);

        public override int GetHashCode()
            => (Group?.GetHashCode() ?? 0)
                ^ (Name?.GetHashCode() ?? 0);

        public override string ToString()
            => $"{{ {nameof(Name)}: \"{Name}\", {nameof(Group)}: \"{Group}\" }}";
    }
}
