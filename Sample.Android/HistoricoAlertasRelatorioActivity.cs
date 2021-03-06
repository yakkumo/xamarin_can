﻿using Android.App;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Sample.Android.Resources.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sample.Android
{

    [Activity(Label = "HistoricoAlertasRelatorioActivity")]
    public class HistoricoAlertasRelatorioActivity : ListActivity
    {
        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoB3.db3");
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var db = new SQLiteAsyncConnection(dbPath);
            var dadosToken = db.Table<Token>();
            var dadosCesv = db.Table<Cesv>();
            var TokenAtual = await dadosToken.Where(x => x.data_att_token >= DateTime.Now).FirstOrDefaultAsync();
            var DadosRelatorioCesv = await dadosCesv.Where(x => x.armazemId == TokenAtual.armazemId && x.cesvId == TokenAtual.cesvId).FirstOrDefaultAsync();

            RelatorioCesv.Add(string.Concat("Numero: ", DadosRelatorioCesv.numero));
            RelatorioCesv.Add(string.Concat("Placa: ", DadosRelatorioCesv.placa));
            RelatorioCesv.Add(string.Concat("Status: ", DadosRelatorioCesv.statusInicio));
            RelatorioCesv.Add(string.Concat("Motorista: ", DadosRelatorioCesv.nome));
            RelatorioCesv.Add(string.Concat("Telefone: ", DadosRelatorioCesv.telefone));
            RelatorioCesv.Add(string.Concat("Cliente: ", DadosRelatorioCesv.nomeCliente));
            RelatorioCesv.Add(string.Concat("Transportadora: ", DadosRelatorioCesv.nomeTransportadora));
            RelatorioCesv.Add(string.Concat("Tipo do veículo: ", DadosRelatorioCesv.tipoVeiculo));
            RelatorioCesv.Add(string.Concat("Data do agendamento: ", DadosRelatorioCesv.dataAgendamentoEntrada));

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.VeiculosSituacaoRelatorio, RelatorioCesv);

            ListView.TextFilterEnabled = true;

        }

        public List<string> RelatorioCesv = new List<string>();
        public List<int> IdCesv = new List<int>();
    }
}


