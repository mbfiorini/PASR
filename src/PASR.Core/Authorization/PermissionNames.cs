namespace PASR.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Activation = "Pages.Users.Activation";
        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Teams = "Pages.Teams"; //Permissão para gerenciar os times de cada Gestor
        public const string Pages_Calls = "Pages.Calls"; //Permissão para alterar dados das Calls
        public const string Pages_Leads = "Pages.Leads"; //Permissão para alterar dados dos Leads
        public const string Delete_Leads = "Delete.Leads"; //Permissão para Deletar
        public const string Update_Leads = "Update.Leads"; //Permissão para alterar dados dos Leads
        public const string Create_Leads = "Create.Leads"; //Permissão para Criar os dados dos Leads
    }
}
