

namespace GeoffsDocusignDemo
{
    public static class GlobalVariables

    {
        // readonly variable
        public static string integratorKey
        {
            get
            {
                //Demo kludge shouuld be a secret kept in DB or in encrypted config
                return "88d8459b-748c-451e-83b8-bc48e5d41ec2";
            }
        }


    }
}
