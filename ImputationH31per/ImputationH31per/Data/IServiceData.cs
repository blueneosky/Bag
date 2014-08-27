using System;
using ImputationH31per.Data.Entite;

namespace ImputationH31per.Data
{
    public interface IServiceData
    {
        string Nom { get; }

        void EnregistrerData(IDatData<IDatTacheTfs, IDatIHFormParametre> data);

        IDatData<IDatTacheTfs, IDatIHFormParametre> ObtenirData();
    }
}