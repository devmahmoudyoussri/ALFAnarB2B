namespace fahim.Contracts
{
    public static class ContractConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Contract." : string.Empty);
        }

    }
}