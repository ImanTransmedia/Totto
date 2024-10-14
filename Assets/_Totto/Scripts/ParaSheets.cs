using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ParaSheets : MonoBehaviour
{
    public string _Nombre;
    public string _Email;
    public string _Mochilas;
    public string _Enemigos;
    public string _Tiempo;

    [SerializeField] private string Base_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfx2zue42vj16eu525lC7xzd9-r0sYvKxWgQ2CNAA7F744vFA/formResponse";

    public void Postear(string Nombre, string Email, string Mochilas, string Enemigos, string Tiempo)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("entry.2003136920", Nombre));
        formData.Add(new MultipartFormDataSection("entry.2011067673", Email));
        formData.Add(new MultipartFormDataSection("entry.1753668045", Mochilas));
        formData.Add(new MultipartFormDataSection("entry.1577896740", Enemigos));
        formData.Add(new MultipartFormDataSection("entry.1633256060", Tiempo));

        UnityWebRequest www = UnityWebRequest.Post(Base_URL, formData);
        var handle = www.SendWebRequest();

        while (!handle.isDone)
            _ = 0;

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al enviar el formulario: " + www.error);
        }
        else
        {
            Debug.Log("Formulario enviado exitosamente");
        }
    }

    public void Send()
    {
        Postear(_Nombre, _Email, _Mochilas, _Enemigos, _Tiempo);
    }
}
