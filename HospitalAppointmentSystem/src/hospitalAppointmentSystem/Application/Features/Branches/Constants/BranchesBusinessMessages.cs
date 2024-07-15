namespace Application.Features.Branches.Constants;

public static class BranchesBusinessMessages
{
    public const string SectionName = "Branch";

    public const string BranchNotExists = "Böyle bir branþ bulunmamaktadýr";

    public static string BranchAlreadyExists = "Bu isimde branþ zaten mevcut";

    public static string CannotDeleteBranchWithDoctors = "Bu branþa ait doktor bulunmaktadýr. Branþ silinemez.";
}