using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using (HttpClient client = new HttpClient())
{
    //Get
    string url = "https://jsonplaceholder.typicode.com/posts";

    client.DefaultRequestHeaders.Clear();

    /*
     * El método GetAsync() envía una solicitud http GET a la URL especificada.
     * El método GetAsync() es asíncrono y devuelve una tarea
     */
    HttpResponseMessage response = client.GetAsync(url).Result;

    /*
     * Se comprueba el estado de la respuesta http utilizando IsSuccessStatusCode
     */
    if (response.IsSuccessStatusCode)
    {
        /*
         * Aquí se está leyendo el contenido de la respuesta HTTP.
         * ReadAsStringAsync()  es una tarea asíncrona que devuelve una cadena que 
         * contiene el contenido de la respuesta.
         * .Result se utiliza para obtener el resultado de la tarea.
         */
        var data = response.Content.ReadAsStringAsync().Result;

        /*
         * Aquí se está convirtiendo la cadena JSON en un objeto JArray
         */
        var posts = JArray.Parse(data);

        foreach (var post in posts)
        {
            Console.WriteLine(post["id"]);
        }
        
        //Console.WriteLine(posts);
    }
    else
    {
        Console.WriteLine("Error: " + response.StatusCode);
    }


    //Post

    string urlPost = "https://jsonplaceholder.typicode.com/posts";

    client.DefaultRequestHeaders.Clear();

    //var postContent = new StringContent(
    //    @"{
    //        ""title"": ""foo"",
    //        ""body"": ""bar"",
    //        ""userId"": 1
    //    }", Encoding.UTF8, "application/json");

    string parameters = "{'title': 'foo', 'body': 'bar', 'userId': '200'}";
    var jsonString = JObject.Parse(parameters);


    /*
     * Aquí se está creando un nuevo objeto StringContent que contiene los datos 
     * que se enviarán en la solicitud POST. Los datos se codifican en UTF8 y 
     * se establece el tipo de medio (media type) en "application/json".
     */
    var httpContent = new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json");


    /*
     * Aquí se está realizando la solicitud POST a la URL especificada y se 
     * está obteniendo la respuesta. 
     * .Result se utiliza para obtener el resultado de la tarea
     */
    HttpResponseMessage responsePost = client.PostAsync(urlPost, httpContent).Result;

    if(responsePost.IsSuccessStatusCode)
    {
        /*
         * Aquí se está leyendo el contenido de la respuesta HTTP. 
         * ReadAsStringAsync() es una tarea asíncrona que devuelve 
         * una cadena que contiene el contenido de la respuesta
         */
        var res2 = responsePost.Content.ReadAsStringAsync().Result;

        /*
         * Aquí se está convirtiendo la cadena JSON en un objeto JObject
         */
        var r = JObject.Parse(res2);

        Console.WriteLine(r);
    }
    else
    {
        Console.WriteLine("Error: " + response.StatusCode);
    }

}
