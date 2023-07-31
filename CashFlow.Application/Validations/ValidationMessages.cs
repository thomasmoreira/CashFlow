namespace CashFlow.Application.Validations
{
    public static class ValidationMessages
    {
        public static string CampoVazio(string nome, string obs = null) => $"O campo {nome} não pode ser vazio. {obs}";
        public static string CampoInvalido(string nome, string obs = null) => $"O campo {nome} não é válido. {obs}";
        public static string CampoTamanhoMaximoInvalido(string nome, int tamanho) => $"O campo {nome} não é válido. Esse campo deve ter no máximo {tamanho} caracteres.";
        public static string CampoTamanhoFixoInvalido(string nome, int tamanho) => $"O campo {nome} não é válido. Esse campo deve ter {tamanho} caractere(s).";
        public static string CampoTamanhoMinimoInvalido(string nome, int tamanho) => $"O campo {nome} não é válido. Esse campo deve ter no mínimo {tamanho} caractere(s).";        

    }
}
