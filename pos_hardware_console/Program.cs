﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CH.Alika.POS.Hardware;

namespace CH.Alika.POS
{
    class Program
    {
        private static String _configFileName = AppDomain.CurrentDomain.BaseDirectory + "AlikaPosConfig.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter key to exit");
            Console.WriteLine();
            using (MMMDocumentScanner scanner = new MMMDocumentScanner())
            using (IScanSink documentSink = new ScanSinkWebService(_configFileName))
            {
                try
                {
                    documentSink.OnScanSinkEvent += HandleScanSinkEvent;
                    scanner.OnCodeLineScanEvent += documentSink.HandleCodeLineScan;
                    scanner.Activate();
                    Console.WriteLine(scanner);
                }
                catch (PosHardwareException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadLine();   
            }
        }

        static void HandleScanSinkEvent(object sender, ScanSinkEvent e)
        {
            Console.WriteLine(e);
        }
    }
}
