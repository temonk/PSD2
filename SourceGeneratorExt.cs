using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace EnumGenerator
{
    public static class SourceGeneratorExt
    {
        public static List<PropertyDeclarationSyntax> GetProperties(this ClassDeclarationSyntax self)
            => self.ChildNodes().OfType<PropertyDeclarationSyntax>().ToList();

        public static bool IsPartial(this ClassDeclarationSyntax self)
        {
            return self.Modifiers.FirstOrDefault(a => a.ValueText == "partial") != default;
        }

        public static List<AttributeSyntax> GetAttributes(this ClassDeclarationSyntax self)
        {
            return self.AttributeLists.SelectMany(a => a.Attributes).ToList();
        }

        public static AttributeSyntax? GetAttributeOfType<TType>(this ClassDeclarationSyntax self)
        {
            var attributes = self.GetAttributes();
            return attributes.Find(a => a.GetAttributeName() == typeof(TType).Name);
        }

        public static AttributeSyntax? GetAttributeOfType<TType>(this PropertyDeclarationSyntax self)
        {
            var attributes = self.GetAttributes();
            return attributes.Find(a => a.GetAttributeName() == typeof(TType).Name);
        }

        public static List<AttributeSyntax> GetAttributes(this PropertyDeclarationSyntax self)
        {
            return self.AttributeLists.SelectMany(a => a.Attributes).ToList();
        }

        public static AttributeSyntax? GetAttributeWithName(this ClassDeclarationSyntax self, string name)
        {
            var attributes = self.GetAttributes();
            return attributes.Find(a => a.GetAttributeName() == name);
        }

        public static string GetAttributeName(this AttributeSyntax self) => self.Name.ToString();

        public static string? GetNamespace(ClassDeclarationSyntax self)
        {
            if (self.Parent is not NamespaceDeclarationSyntax ns)
                return null;

            return ns.GetNamespace();
        }

        public static string GetNamespace(this NamespaceDeclarationSyntax self) => self.Name.ToString();

        public static string GetIdentifierName(this BaseTypeDeclarationSyntax self) => self.Identifier.ValueText;
    }
}