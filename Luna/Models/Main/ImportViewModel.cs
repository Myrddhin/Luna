using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Loki.Commands;
using Loki.Common;
using Loki.UI;
using Loki.UI.Tasks;
using Luna.Model.CRM;
using Luna.Model.IO;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Luna.UI.Main
{
    public class ImportViewModel : DocumentScreen
    {
        public ImportViewModel()
        {
            DisplayName = "Importation de contacts";
            Contacts = new BindableCollection<Contact>();
            Tags = new BindableCollection<Tag>();
        }

        private struct DataConverterParams
        {
            public string FileName { get; set; }

            public IOMode Mode { get; set; }
        }

        public IDataConverterModule DataConverter { get; set; }

        public ICommand Import { get; private set; }

        public ICommand GenerateFile { get; private set; }

        public BindableCollection<Contact> Contacts { get; private set; }

        public BindableCollection<Tag> Tags { get; private set; }

        private ITaskConfiguration<DataConverterParams, IEnumerable<DataConverterLog>> exportRunner;

        #region ImportResult

        private PropertyChangedEventArgs argsimportResultChanged = ObservableHelper.CreateChangedArgs<ImportViewModel>(x => x.ImportResult);

        private string importResult;

        public string ImportResult
        {
            get
            {
                return importResult;
            }

            set
            {
                if (!object.Equals(importResult, value))
                {
                    importResult = value;
                    NotifyChanged(argsimportResultChanged);
                }
            }
        }

        #endregion ImportResult

        protected override void OnInitialize()
        {
            base.OnInitialize();

            exportRunner = CreateWorker<DataConverterParams, IEnumerable<DataConverterLog>>("Export", Export_DoWork);

            Import = Commands.Create();
            GenerateFile = Commands.Create();

            Commands.Handle(Import, Import_Execute);
            Commands.Handle(GenerateFile, GenerateFile_CanExecute, GenerateFile_Execute);
            Commands.Handle(ApplicationCommands.Save, Save_CanExecute, Save_Execute);
        }

        private void Save_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
        }

        private void Import_Execute(object sender, CommandEventArgs e)
        {
        }

        private async Task<IEnumerable<DataConverterLog>> Export_DoWork(DataConverterParams param)
        {
            return await DataConverter.GenerateFile(param.FileName, param.Mode);
        }

        private void Export_Complete(IEnumerable<DataConverterLog> log)
        {
        }

        private void GenerateFile_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
            e.CanExecute = !exportRunner.IsRunning;
        }

        private async void GenerateFile_Execute(object sender, CommandEventArgs e)
        {/*
            FileDialogInformations infos = new FileDialogInformations();
            infos.DefaultExtension = ".xlsx";
            infos.Filter = "Fichier excel (*.xlsx)|*.xlsx";
            var filePath = Windows.GetSaveFileName(infos);
            if (!string.IsNullOrEmpty(filePath) && !exportRunner.IsRunning)
            {
                var exportResult = await exportRunner.DoWorkAsync(new DataConverterParams() { Mode = IOMode.StandardExcel, FileName = filePath });
                if (exportResult != null)
                {
                    int a = exportResult.Count();
                    Process.Start(filePath);
                }
            }*/

            // Get token
            AuthenticationContext ac = new AuthenticationContext(
              "https://login.windows.net/lunacloudapps.fr");
            AuthenticationResult ar =
              ac.AcquireToken("https://lunacloudapps/LunaWebApi", "fd185a5f-9d4e-4681-be60-009cfeedb958", new Uri("https://lunacloudapps.fr/luna"));
            // Call Web API
            string authHeader = ar.CreateAuthorizationHeader();
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(
              HttpMethod.Get, "http://luna-web.azurewebsites.net/api/Values");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);
            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseString);
        }

        private void Save_Execute(object sender, CommandEventArgs e)
        {
            DataConverter.SaveChangesAsync();
        }
    }
}