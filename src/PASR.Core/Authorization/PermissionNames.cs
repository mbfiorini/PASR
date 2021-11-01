namespace PASR.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Administration = "Pages.Administration";
        public const string Pages_Tenants = "Pages.Tenants";

        #region Users
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Activation = "Pages.Users.Activation";
        public const string Update_Users = "Update.Users"; //Permissão para deletar/alterar dados dos Usuários
        public const string List_Users = "List.Users"; //Permissão para deletar/alterar dados dos Usuários
        #endregion

        #region Roles
        public const string Pages_Roles = "Pages.Roles";
        public const string List_Roles = "List.Roles";
        public const string Update_Roles = "Update.Roles";
        #endregion

        #region Teams
        public const string Pages_Teams = "Pages.Teams"; //Permissão para gerenciar os times de cada Gestor
        public const string List_Teams = "List.Teams"; //Permissão para gerenciar os times de cada Gestor
        public const string Update_Teams = "Update.Teams"; //Permissão para gerenciar os times de cada Gestor
        #endregion
        
        #region Calls
        public const string Pages_Calls = "Pages.Calls"; //Permissão para alterar dados das Calls
        public const string List_Calls = "List.Calls"; //Permissão para alterar dados das Calls
        public const string Update_Calls = "Update.Calls"; //Permissão para alterar dados das Calls
        public const string Create_Calls = "Create.Calls"; //Permissão para alterar dados das Calls
        #endregion

        #region Leads
        public const string Pages_Leads = "Pages.Leads"; //Permissão para alterar dados dos Leads
        public const string Create_Leads = "Create.Leads"; //Permissão para Criar os dados dos Leads
        public const string Update_Leads = "Update.Leads"; //Permissão para alterar dados dos Leads
        public const string List_Leads = "List.Leads"; //Permissão para alterar dados dos Leads
        
        #endregion
    }
}
