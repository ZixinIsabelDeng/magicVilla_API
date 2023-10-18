namespace magicVilla_VillaAPI.Logging
{
    public class Logging:ILogging
    {

        public void Log(String message, string type)
        {
            if (type == "error")
                {
                Console.WriteLine("ERROR - " + message);
            }
            else 
            {
                Console.WriteLine(message);   
            }
        
        }

    }
}
