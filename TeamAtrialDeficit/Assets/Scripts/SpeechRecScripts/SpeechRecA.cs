using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System;

public class SpeechRecA : MonoBehaviour
{
    public Text outputText;

    //This will need to be replaced with your own key.
    private string subscriptionKey = "21e25a398508422c9b721319afefd550";
    private string token;
    //length of any recording sent. 10s is the current limit.
    public int recordDuration = 5;

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    private void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
    }

    //These structures contain the relevant fields from the response string/JSON that is returned by the service
    [System.Serializable]
    class AssemblyResponse
    {
        public string id;
        public string status;
        public string text;
    };
    [System.Serializable]
    class AssemblyUploadResponse
    {
        //public string upload url;
    };
}
