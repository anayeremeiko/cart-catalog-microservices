namespace Catalog.Core.Validators
{
	internal static class CustomRules
	{
		internal static bool BeAValidUrl(string parameter)
		{
			Uri result;

			return Uri.TryCreate(parameter, UriKind.Absolute, out result);
		}
	}
}
