/*
 BLUESKY API
 HELPERS UTILITY CLASS
 CERTIFICATE WHORE
 v1.0
 LAST EDITED: SATURDAY FEBRUARY 18, 2023
 COPYRIGHT © TECH SKULL STUDIOS
*/

using UnityEngine.Networking;

namespace Bluesky.Helpers
{
    public class CertificateWhore : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}