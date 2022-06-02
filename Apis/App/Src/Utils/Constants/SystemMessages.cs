namespace InteliSystem.Utils.Constants;

public class SystemMessages
{
    private const string _requestProblem = "Ocorreu um problema na sua solicitação. Favor tentar novamente";
    private const string _countryNotInformed = "País não informado";
    private const string _failToDeleteImage = "Falha ao tentar deletar a Imagem";
    private const string _failToSaveImage = "Falha ao tentar salvaar a Imagem";
    private const string _imageNotFound = "Imagem não encontrada";
    private const string _registerNotFound = "Nenhum registro encontrado";

    public static string RequestProblem => _requestProblem;

    public static string CountryNotInformed => _countryNotInformed;

    public static string FailToDeleteImage => _failToDeleteImage;

    public static string ImageNotFound => _imageNotFound;

    public static string RegisterNotFound => _registerNotFound;

    public static string FailToSaveImage => _failToSaveImage;
}