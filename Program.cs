/* ************************************************************************ *\
 *                                                                          *
 *      Project:    Picture Viewer DE                                       *
 *      Author:     Le Huu, Etienne-Minh                                    *
 *      SN:         2211757                                                 *
 *                                                                          *
 *                   Copyright© 2022 - Le Huu, Etienne-Minh - Copywrong! :( *
\* ************************************************************************ */
using System;
using System.Windows.Forms;

namespace PictureViewerDE
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

/* *** Notes *** *\

// Les sanglots longs Des violons De l'automne. Blessent mon coeur D'une langueur Monotone.
https://en.wikipedia.org/wiki/Chanson_d%27automne#Use_in_World_War_II
In World War II lines from the poem were used to send messages from
Special Operations Executive (SOE) to the French Resistance about the
timing of the forthcoming Invasion of Normandy. 

    Les sanglots longs
    Des violons

        De l'automne

    Blessent mon cœur
    D'une langueur

        Monotone.

    Tout suffocant
    Et blême, quand

        Sonne l'heure,

    Je me souviens
    Des jours anciens

        Et je pleure;

    Et je m'en vais
    Au vent mauvais

        Qui m'emporte

    Deçà, delà,
    Pareil à la

        Feuille morte.
\* *** Notes *** */