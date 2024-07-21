using System.Text.RegularExpressions;

namespace AStore_Web.Models
{
	public static class SLugExtentions
	{
		public static string GenerateSlug(this string value)
		{
			value = value.ToLowerInvariant();
			value = Regex.Replace(value, @"\s+", "-"); // Replace spaces with hyphens
			value = Regex.Replace(value, @"[^a-z0-9\s-]", ""); // Remove invalid characters
			value = Regex.Replace(value, @"-+", "-"); // Replace multiple hyphens with a single hyphen
			value = value.Trim('-'); // Trim leading and trailing hyphens
			return value;
		}
	}
}
