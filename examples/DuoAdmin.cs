/*
 * Copyright (c) 2013 Duo Security
 * All rights reserved, all wrongs reversed.
 */

using Duo;
using System;
using System.Collections.Generic;

class DuoAdmin
{
    static int Main(string[] args)
    {
        if (args.Length < 3)
        {
            System.Console.WriteLine("Usage: <integration key> <secret key> <integration host>");
            return 1;
        }
        string ikey = args[0];
        string skey = args[1];
        string host = args[2];
        var client = new Duo.DuoApi(ikey, skey, host);
        var parameters = new Dictionary<string, string>();
        var r = client.JSONApiCall<Dictionary<string, object>>(
            "GET", "/admin/v1/info/authentication_attempts", parameters);
        var attempts = r["authentication_attempts"] as Dictionary<string, object>;
        foreach (KeyValuePair<string, object> info in attempts) {
            var s = String.Format("{0} authentication(s) ended with {1}.",
                                  info.Value,
                                  info.Key);
            System.Console.WriteLine(s);
        }

        return 0;
    }
}

