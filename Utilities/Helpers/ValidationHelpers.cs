namespace Utilities.Helpers;

public static class ValidationHelpers
{
    public static bool CheckProfanity(string groupName)
    {
        var filter = new ProfanityFilter.ProfanityFilter();
        filter.AllowList.Add("hot");
        var detected = filter.DetectAllProfanities(groupName);
        return detected.Count == 0;
    }
}
