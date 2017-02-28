using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_All
{
    class Program
    {
        public static KeyboardReader reader = new KeyboardReader();

        //  public static String rootFolder = System.getProperty("user.dir") + File.separator +"bank" + File.separator ;
        public static String rootFolder = ("C:\\CloudCoins" + Path.DirectorySeparatorChar);
        public static String importFolder = (rootFolder + "Import" + Path.DirectorySeparatorChar);
        public static String importedFolder = (rootFolder + "Imported" + Path.DirectorySeparatorChar);
        public static String trashFolder = (rootFolder + "Trash" + Path.DirectorySeparatorChar);
        public static String suspectFolder = (rootFolder + "Suspect" + Path.DirectorySeparatorChar);
        public static String frackedFolder = (rootFolder + "Fracked" + Path.DirectorySeparatorChar);
        public static String bankFolder = (rootFolder + "Bank" + Path.DirectorySeparatorChar);
        public static String templateFolder = (rootFolder + "Templates" + Path.DirectorySeparatorChar);
        public static String counterfeitFolder = (rootFolder + "Counterfeit" + Path.DirectorySeparatorChar);
        public static String directoryFolder = (rootFolder + "Directory" + Path.DirectorySeparatorChar);
        public static String exportFolder = (rootFolder + "Export" + Path.DirectorySeparatorChar);
        public static String prompt = "RAIDA Tester";
        public static String[] commandsAvailable = new String[] { "test one", "test all", "quit" };
        //public static String[] commandsAvailable = new String[] { "import", "show coins", "export", "fix fracked", "quit", "show folders", "test echo", "test detect", "test get_ticket", "test hints", "test fix", };

        public static int timeout = 10000;
        public static String testCoinName = AppDomain.CurrentDomain.BaseDirectory + "testcoin.stack";

        // Milliseconds to wait until the request is ended. 
        public static FileUtils fileUtils = new FileUtils(rootFolder, importFolder, importedFolder, trashFolder, suspectFolder, frackedFolder, bankFolder, templateFolder, counterfeitFolder, directoryFolder, exportFolder);

        public static Random myRandom = new Random();
        public static String echohtml;
        public static String detecthtml;
        public static String get_tickethtml;
        public static String hintshtml;
        public static String fix1html;

        // This is used for naming new chests
        public static void Main(String[] args)
        {
            echohtml = File.ReadAllText("echotemplate.html");
            detecthtml = File.ReadAllText("detecttemplate.html");
            get_tickethtml = File.ReadAllText("tickettemplate.html");
            hintshtml = File.ReadAllText("hintstemplate.html");
            fix1html = File.ReadAllText("fix1template.html");

            // Console.SetWindowSize(210, 74);
            printWelcome();
            run();
            // Makes all commands available and loops
            Console.Out.WriteLine("Thank you for using RAIDA Tester Complete. Goodbye.");
        }

        // End main
        public static void run()
        {
            bool restart = false;

            while (!restart)
            {
                Console.Out.WriteLine("========================================");
                Console.Out.WriteLine(prompt + " Commands Available:");
                int commandCounter = 1;
                foreach (String command in commandsAvailable)
                {
                    Console.Out.WriteLine(commandCounter + (". " + command));
                    commandCounter++;
                }

                Console.Out.Write(prompt + ">");
                String commandRecieved = reader.readString(commandsAvailable);
                switch (commandRecieved.ToLower())
                {
                    case "test one":
                        Console.Out.WriteLine("What RAIDA # do you want to test?");
                        Console.Out.Write("Enter RIADA #>");
                        int raidaID = reader.readInt(0, 25);
                        testOne(raidaID);
                        break;
                    case "test all":
                        testAll();
                        break;
                    case "show coins":
                        showCoins();
                        break;
                    case "import":
                        // importCoins();
                        break;
                    case "export":
                        // export();
                        break;
                    case "fix fracked":
                        //fixFracked();
                        break;
                    case "show folders":
                        showFolders();
                        break;
                    case "quit":
                        Console.Out.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Out.WriteLine("Command failed. Try again.");
                        break;
                }
                // end switch
            }

            // end while
        }

        // end run method
        public static void printWelcome()
        {
            Console.Out.WriteLine("Welcome to RAIDA Tester. A CloudCoin Consortium Opensource.");
            Console.Out.WriteLine("The Software is provided as is, with all faults, defects and errors, and without warranty of any kind.");
            Console.Out.WriteLine("You must have an authentic CloudCoin .stack file called 'testcoin.stack' in the same folder as this program to run tests.");
            Console.Out.WriteLine("The test coin will not be written to.");
        }

        // End print welcome
        public static void showCoins()
        {
            // This is for consol apps.
            Banker bank = new Banker(fileUtils);
            int[] bankTotals = bank.countCoins(bankFolder);
            int[] frackedTotals = bank.countCoins(frackedFolder);
            // int[] counterfeitTotals = bank.countCoins( counterfeitFolder );
            Console.Out.WriteLine("Your Bank Inventory:");
            int grandTotal = (bankTotals[0] + frackedTotals[0]);
            Console.Out.WriteLine(("Total: " + grandTotal));
            Console.Out.WriteLine("  1s: " + (bankTotals[1] + frackedTotals[1]));
            Console.Out.WriteLine("  5s: " + (bankTotals[2] + frackedTotals[2]));
            Console.Out.WriteLine(" 25s: " + (bankTotals[3] + frackedTotals[3]));
            Console.Out.WriteLine("100s: " + (bankTotals[4] + frackedTotals[4]));
            Console.Out.WriteLine("250s: " + (bankTotals[5] + frackedTotals[5]));
        }

        // end show
        public static void showFolders()
        {
            Console.Out.WriteLine(("Your Root folder is " + rootFolder));
            Console.Out.WriteLine(("Your Import folder is " + importFolder));
            Console.Out.WriteLine(("Your Imported folder is " + importedFolder));
            Console.Out.WriteLine(("Your Suspect folder is " + suspectFolder));
            Console.Out.WriteLine(("Your Trash folder is " + trashFolder));
            Console.Out.WriteLine(("Your Bank folder is " + bankFolder));
            Console.Out.WriteLine(("Your Fracked folder is " + frackedFolder));
            Console.Out.WriteLine(("Your Templates folder is " + templateFolder));
            Console.Out.WriteLine(("Your Directory folder is " + directoryFolder));
            Console.Out.WriteLine(("Your Counterfeits folder is " + counterfeitFolder));
            Console.Out.WriteLine(("Your Export folder is " + exportFolder));
        }
        /*
                // end show folders
                public static void importCoins()
                {
                    Console.Out.WriteLine(("Loading all CloudCoins in your import folder:" + importFolder));
                    Importer importer = new Importer(fileUtils);
                    if (!importer.importAll())
                    {
                        return;
                        // There were no file to import
                    }

                    // Move all coins to seperate JSON files in the the suspect folder.
                    Detector detector = new Detector(fileUtils, 10000);
                    int[] detectionResults = detector.detectAll();
                    Console.Out.WriteLine(("Total Received in bank: "
                                    + (detectionResults[0] + detectionResults[2])));
                    // And the bank and the fractured for total
                    Console.Out.WriteLine(("Total Counterfeit: " + detectionResults[1]));
                    CommandInterpreter.showCoins();
                }

                // end import
                public static void export()
                {
                    Banker bank = new Banker(fileUtils);
                    int[] bankTotals = bank.countCoins(bankFolder);
                    int[] frackedTotals = bank.countCoins(frackedFolder);
                    Console.Out.WriteLine("Your Bank Inventory:");
                    int grandTotal = (bankTotals[0] + frackedTotals[0]);
                    Console.Out.WriteLine(("Total: " + grandTotal));
                    Console.Out.Write(("  1s: "
                                    + ((bankTotals[1] + frackedTotals[1])
                                    + " || ")));
                    Console.Out.Write(("  5s: "
                                    + ((bankTotals[2] + frackedTotals[2])
                                    + " ||")));
                    Console.Out.Write((" 25s: "
                                    + ((bankTotals[3] + frackedTotals[3])
                                    + " ||")));
                    Console.Out.Write(("100s: "
                                    + ((bankTotals[4] + frackedTotals[4])
                                    + " ||")));
                    Console.Out.WriteLine(("250s: "
                                    + (bankTotals[5] + frackedTotals[5])));
                    // state how many 1, 5, 25, 100 and 250
                    int exp_1 = 0;
                    int exp_5 = 0;
                    int exp_25 = 0;
                    int exp_100 = 0;
                    int exp_250 = 0;
                    Console.Out.WriteLine("Do you want to export your CloudCoin to (1)jpgs or (2) stack (JSON) file?");
                    int file_type = reader.readInt(1, 2);
                    // 1 jpg 2 stack
                    if ((bankTotals[1] + frackedTotals[1]) > 0)
                    {
                        Console.Out.WriteLine("How many 1s do you want to export?");
                        exp_1 = reader.readInt(0, (bankTotals[1] + frackedTotals[1]));
                    }

                    // if 1s not zero 
                    if ((bankTotals[2] + frackedTotals[2]) > 0)
                    {
                        Console.Out.WriteLine("How many 5s do you want to export?");
                        exp_5 = reader.readInt(0, (bankTotals[2] + frackedTotals[2]));
                    }

                    // if 1s not zero 
                    if ((bankTotals[3] + frackedTotals[3] > 0))
                    {
                        Console.Out.WriteLine("How many 25s do you want to export?");
                        exp_25 = reader.readInt(0, (bankTotals[3] + frackedTotals[3]));
                    }

                    // if 1s not zero 
                    if ((bankTotals[4] + frackedTotals[4]) > 0)
                    {
                        Console.Out.WriteLine("How many 100s do you want to export?");
                        exp_100 = reader.readInt(0, (bankTotals[4] + frackedTotals[4]));
                    }

                    // if 1s not zero 
                    if ((bankTotals[5] + frackedTotals[5]) > 0)
                    {
                        Console.Out.WriteLine("How many 250s do you want to export?");
                        exp_250 = reader.readInt(0, (bankTotals[5] + frackedTotals[5]));
                    }

                    // if 1s not zero 
                    // move to export
                    Exporter exporter = new Exporter(fileUtils);
                    Console.Out.WriteLine("What tag will you add to the file?");
                    String tag = reader.readString(false);
                    Console.Out.WriteLine(("Exporting to:" + exportFolder));
                    if ((file_type == 1))
                    {
                        exporter.writeJPEGFiles(exp_1, exp_5, exp_25, exp_100, exp_250, tag);
                        // stringToFile( json, "test.txt");
                    }
                    else
                    {
                        exporter.writeJSONFile(exp_1, exp_5, exp_25, exp_100, exp_250, tag);
                    }

                    // end if type jpge or stack
                    Console.Out.WriteLine("Exporting CloudCoins Completed.");
                }

                // end export One
                public static void fixFracked()
                {
                    // Load coins from file in to banks fracked array
                    Frack_Fixer frack_fixer = new Frack_Fixer(fileUtils, timeout);
                    Banker bank = new Banker(fileUtils);
                    int[] frackedTotals = bank.countCoins((rootFolder + frackedFolder));
                    Console.Out.WriteLine(("You  have "
                                    + (frackedTotals[0] + " fracked coins.")));
                    int[] frackedResults = frack_fixer.fixAll();
                    // REPORT ON DETECTION OUTCOME
                    Console.Out.WriteLine("Results of Fix Fractured:");
                    Console.Out.WriteLine(("Good and Moved in Bank: " + frackedResults[0]));
                    Console.Out.WriteLine(("Counterfeit and Moved to trash: " + frackedResults[1]));
                    Console.Out.WriteLine(("Still Fracked and Moved to Fracked: " + frackedResults[2]));
                }//end fix fracked
        */
        public static void testEcho(int raidaID)
        {
            // File.WriteAllText(saveName, fileText);
            string statusTag = "[" + raidaID + "RAIDASTATUS]";
            string msTag = "[" + raidaID + "RAIDAMS]";
            string requestTag = "[" + raidaID + "RAIDAREQUEST]";
            string responseTag = "[" + raidaID + "RAIDARESPONSE]";
            DetectionAgent da = new DetectionAgent(raidaID, 5000);
            da.echo(raidaID);
            string raidastatus = "";

            if (da.lastResponse.Contains("ready"))
            {
                raidastatus = "<span style='color:green;'>ready</span>";
                Console.ForegroundColor = ConsoleColor.Green;
                echohtml = echohtml.Replace(statusTag, raidastatus);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                raidastatus = "<span style='color:red;'>Fail</span>";
                echohtml = echohtml.Replace(statusTag, raidastatus);
            }//end if pass
            Console.Out.WriteLine("Status: " + da.lastResponse);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine("Milliseconds: " + da.dms);
            Console.Out.WriteLine("Request: ");
            Console.Out.WriteLine(da.lastRequest);
            Console.Out.WriteLine("Response: ");
            Console.Out.WriteLine(da.lastResponse);
            string ms = "Milliseconds: " + da.dms.ToString();
            echohtml = echohtml.Replace(msTag, ms);
            echohtml = echohtml.Replace(requestTag, da.lastRequest);
            echohtml = echohtml.Replace(responseTag, da.lastResponse);
        }//end test echo

        public static void testDetect(int raidaID)
        {
            // File.WriteAllText(saveName, fileText);
            string statusTag = "[" + raidaID + "RAIDASTATUS]";
            string msTag = "[" + raidaID + "RAIDAMS]";
            string requestTag = "[" + raidaID + "RAIDAREQUEST]";
            string responseTag = "[" + raidaID + "RAIDARESPONSE]";
            Console.Out.WriteLine("Loading test coin: " + testCoinName);
            CloudCoin testCoinDetect = fileUtils.loadOneCloudCoinFromJsonFile(testCoinName);
            string raidastatus = "";
            DetectionAgent da = new DetectionAgent(raidaID, 5000);
            da.detect(testCoinDetect.nn, testCoinDetect.sn, testCoinDetect.ans[raidaID], testCoinDetect.ans[raidaID], testCoinDetect.getDenomination());
            if (da.lastResponse.Contains("pass"))
            {
                raidastatus = "<span style='color:green;'>ready</span>";
                Console.ForegroundColor = ConsoleColor.Green;
                detecthtml = detecthtml.Replace(statusTag, raidastatus);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                raidastatus = "<span style='color:red;'>Fail</span>";
                detecthtml = detecthtml.Replace(statusTag, raidastatus);
            }//end if pass
            Console.Out.WriteLine("Status: " + da.lastDetectStatus);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine("Milliseconds: " + da.dms);
            Console.Out.WriteLine("Request: ");
            Console.Out.WriteLine(da.lastRequest);
            Console.Out.WriteLine("Response: ");
            Console.Out.WriteLine(da.lastResponse);
            Console.Out.WriteLine("=======================================================");

            string ms = "Milliseconds: " + da.dms.ToString();
            detecthtml = detecthtml.Replace(msTag, ms);
            detecthtml = detecthtml.Replace(requestTag, da.lastRequest);
            detecthtml = detecthtml.Replace(responseTag, da.lastResponse);

        }//end test echo


        public static void testGetTicket(int raidaID)
        {
            string statusTag = "[" + raidaID + "RAIDASTATUS]";
            string msTag = "[" + raidaID + "RAIDAMS]";
            string requestTag = "[" + raidaID + "RAIDAREQUEST]";
            string responseTag = "[" + raidaID + "RAIDARESPONSE]";
            Console.Out.WriteLine("Loading test coin: " + testCoinName);
            CloudCoin testCoin = fileUtils.loadOneCloudCoinFromJsonFile(testCoinName);
            string raidastatus = "";


            DetectionAgent da = new DetectionAgent(raidaID, 5000);
            da.get_ticket(testCoin.nn, testCoin.sn, testCoin.ans[raidaID], testCoin.getDenomination());

            if (da.lastTicketStatus == "ticket")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                raidastatus = "<span style='color:green;'>Pass</span>";
                get_tickethtml = get_tickethtml.Replace(statusTag, raidastatus);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                raidastatus = "<span style='color:red;'>Fail</span>";
                get_tickethtml = get_tickethtml.Replace(statusTag, raidastatus);
            }//end if pass

            Console.Out.WriteLine("Status: " + da.lastTicketStatus);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine("Milliseconds: " + da.dms);
            Console.Out.WriteLine("Request: ");
            Console.Out.WriteLine(da.lastRequest);
            Console.Out.WriteLine("Response: ");
            Console.Out.WriteLine(da.lastResponse);
            Console.Out.WriteLine("=======================================================");
            string ms = "Milliseconds: " + da.dms.ToString();
            get_tickethtml = get_tickethtml.Replace(msTag, ms);
            get_tickethtml = get_tickethtml.Replace(requestTag, da.lastRequest);
            get_tickethtml = get_tickethtml.Replace(responseTag, da.lastResponse);

        }//end test echo


        public static void testHints(int raidaID)
        {
            string statusTag = "[" + raidaID + "RAIDASTATUS]";
            string msTag = "[" + raidaID + "RAIDAMS]";
            string requestTag = "[" + raidaID + "RAIDAREQUEST]";
            string responseTag = "[" + raidaID + "RAIDARESPONSE]";
            Console.Out.WriteLine("Loading test coin: " + testCoinName);
            CloudCoin testCoin = fileUtils.loadOneCloudCoinFromJsonFile(testCoinName);

            DetectionAgent da = new DetectionAgent(raidaID, 5000);
            da.testHint(testCoin.nn, testCoin.sn, testCoin.ans[raidaID], testCoin.getDenomination());
            string raidastatus = "";
            if (da.lastTicketStatus != "ticket")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Could not Check hints because get_ticket service failed to get a ticket. Fix get_ticket first. Status: " + da.lastTicketStatus);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine("Milliseconds: " + da.dms);
                Console.Out.WriteLine("Request: ");
                Console.Out.WriteLine(da.lastRequest);
                Console.Out.WriteLine("Response: ");
                Console.Out.WriteLine(da.lastResponse);
                Console.Out.WriteLine("=======================================================");
                raidastatus = "<span style='color:red;'>Fail</span>";
                hintshtml = hintshtml.Replace(statusTag, raidastatus);

                return;

            }


            if (da.lastResponse.Contains("-2"))
            {
                // -2 means not ticket found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error -2, Your server could not find the same ticket that it created itself. Make sure your server's hints.php is working.");
                Console.ForegroundColor = ConsoleColor.White;
                raidastatus = "<span style='color:red;'>Error -2, Your server could not find the same ticket that it created itself. Make sure your server's hints.php is working</span>";
                hintshtml = hintshtml.Replace(statusTag, raidastatus);
            }
            else if (da.lastResponse.Contains("-3"))
            {
                // -2 means ticket was not the correct lenght (44 characters)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error -3, The ticket returned was not the required 44 charactrs long.");
                Console.ForegroundColor = ConsoleColor.White;
                raidastatus = "<span style='color:red;'>Error -3, The ticket returned was not the required 44 charactrs long.";
                hintshtml = hintshtml.Replace(statusTag, raidastatus);

            }
            else if (da.lastResponse.Contains(":"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                String[] parts = da.lastResponse.Split(':');
                Console.WriteLine("Sucess: The serial number was " + parts[0] + " and the ticket age was " + parts[1] + " milliseconds old.");
                Console.ForegroundColor = ConsoleColor.White;
                raidastatus = "<span style='color:green;'>Sucess: The serial number was " + parts[0] + " and the ticket age was " + parts[1] + " milliseconds old.";
                hintshtml = hintshtml.Replace(statusTag, raidastatus);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + da.lastResponse);
                Console.ForegroundColor = ConsoleColor.White;
                raidastatus = "<span style='color:red;'>Error: " + da.lastResponse + "</span>";
                hintshtml = hintshtml.Replace(statusTag, raidastatus);
            }
            Console.Out.WriteLine("Request: ");
            Console.Out.WriteLine(da.lastRequest);
            Console.Out.WriteLine("Response: ");
            Console.Out.WriteLine(da.lastResponse);
            string ms = "Milliseconds: " + da.dms.ToString();
            hintshtml = hintshtml.Replace(msTag, ms);
            hintshtml = hintshtml.Replace(requestTag, da.lastRequest);
            hintshtml = hintshtml.Replace(responseTag, da.lastResponse);
            Console.Out.WriteLine("=======================================================");
            // System.out.println("What RAIDA # do you want to get ticket? Enter 25 to end");

        }//end test hints


        public static void testFix(int raidaID, int cornerID)
        {
            string statusTag = "[" + raidaID + "RAIDASTATUS]";
            string msTag = "[" + raidaID + "RAIDAMS]";
            string requestTag = "[" + raidaID + "RAIDAREQUEST]";
            string responseTag = "[" + raidaID + "RAIDARESPONSE]";
            string ms = "";

            String raidastatus = "";
            CloudCoin testCoin = fileUtils.loadOneCloudCoinFromJsonFile(testCoinName);
            FixitHelper fixHelp = new FixitHelper(raidaID, testCoin.ans);
            RAIDA raidaArray = new RAIDA(5000);
            fixHelp.setCornerToTest(cornerID);
            DateTime before = DateTime.Now;
            String[] hasTickets = raidaArray.get_tickets(fixHelp.currentTriad, fixHelp.currentAns, testCoin.nn, testCoin.sn, testCoin.getDenomination()); //This test uses coin Network number 3, sn number 3 and denomination 1 to do the test. 

            Console.Out.WriteLine("Status Results:" + hasTickets[0] + ", " + hasTickets[1] + ", " + hasTickets[2]);
            if (hasTickets[0] == "ticket" && hasTickets[1] == "ticket" && hasTickets[2] == "ticket")//Did the get_tickets() method return all good?
            {
                DetectionAgent da = new DetectionAgent(raidaID, 5000);
                
               // da.fix( triad  m1, m2, m3, pan);
                raidaArray.agent[raidaID].fix(fixHelp.currentTriad, raidaArray.agent[fixHelp.currentTriad[0]].lastTicket, raidaArray.agent[fixHelp.currentTriad[1]].lastTicket, raidaArray.agent[fixHelp.currentTriad[2]].lastTicket, testCoin.ans[raidaID]);
                DateTime after = DateTime.Now;
                TimeSpan ts = after.Subtract(before);

                if (raidaArray.agent[raidaID].lastFixStatus == "success")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("RAIDA" + raidaID + " Success!");
                    Console.Out.WriteLine("Time spent: " + after.Millisecond + " milliseconds.");
                    Console.ForegroundColor = ConsoleColor.White;
                    raidastatus = "<span style='color:green;'>Success</span>";
                    fix1html = fix1html.Replace(statusTag, raidastatus);
                    ms = "Milliseconds: " + raidaArray.agent[raidaID].dms.ToString();
                    fix1html = fix1html.Replace(msTag, ms);
                    fix1html = fix1html.Replace(requestTag, raidaArray.agent[raidaID].lastRequest);
                    fix1html = fix1html.Replace(responseTag, raidaArray.agent[raidaID].lastResponse);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to fix");
                    Console.ForegroundColor = ConsoleColor.White;
                    raidastatus = "<span style='color:red;'>Error: Failed to fix</span>";
                    fix1html = fix1html.Replace(statusTag, raidastatus);
                    ms = "Milliseconds: " + da.dms.ToString();
                    fix1html = fix1html.Replace(msTag, ms);
                    fix1html = fix1html.Replace(requestTag, raidaArray.agent[raidaID].lastRequest);
                    fix1html = fix1html.Replace(responseTag, raidaArray.agent[raidaID].lastResponse);
                }
                Console.Out.WriteLine("Request: ");
                Console.Out.WriteLine(raidaArray.agent[raidaID].lastRequest);
                Console.Out.WriteLine("Response: ");
                Console.Out.WriteLine(raidaArray.agent[raidaID].lastResponse);
            }
            else
            {//No tickets, go to next triad
                Console.Out.WriteLine("Trusted Servers failed to vouch for RAIDA" + raidaID + ". Fix may still work with another triad of trusted servers.");
                raidastatus = "<span style='color:red;'>Trusted Servers failed to vouch for RAIDA" + raidaID + ". Fix may still work with another triad of trusted servers.</span>";
                ms = "Milliseconds: 000";
                fix1html = fix1html.Replace(msTag, ms);
                fix1html = fix1html.Replace(statusTag, raidastatus);
                fix1html = fix1html.Replace(requestTag, "No Request Could be made: Not enough tickets.");
                fix1html = fix1html.Replace(responseTag, "No Response was recieved: Not enough tickets.");
            }//all the tickets are good. 

        }//end test fix it test


        public static void testOne(int RaidaID)
        {

        }//end test one


        public static void testAll()
        {
            DateTime time = DateTime.Now;             // Use current time.
            string format = "MMM ddd d HH:mm yyyy";   // Use this format.
            for (int i = 0; i < 25; i++)
            {
                testEcho(i);
            }
            //testDetect();
            echohtml = echohtml.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("echotest.html", echohtml);

            for (int i = 0; i < 25; i++)
            {
                testDetect(i);
            }
            //testDetect();
            detecthtml = detecthtml.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("detecttest.html", detecthtml);



            for (int i = 0; i < 25; i++)
            {
                testGetTicket(i);
            }
            //testDetect();
            get_tickethtml = get_tickethtml.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("tickettest.html", get_tickethtml);


            for (int i = 0; i < 25; i++)
            {
                testHints(i);
            }
            //testDetect();
            hintshtml = hintshtml.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("hintstest.html", hintshtml);

            for (int i = 0; i < 25; i++)
            {
                testFix(i, 1);
            }
            fix1html = fix1html.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("fix1test.html", fix1html);


            fix1html = File.ReadAllText("fix2template.html");
            for (int i = 0; i < 25; i++)
            {
                testFix(i, 2);
            }
            fix1html = fix1html.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("fix2test.html", fix1html);

            fix1html = File.ReadAllText("fix3template.html");
            for (int i = 0; i < 25; i++)
            {
                testFix(i, 3);
            }
            fix1html = fix1html.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("fix3test.html", fix1html);

            fix1html = File.ReadAllText("fix4template.html");
            for (int i = 0; i < 25; i++)
            {
                testFix(i, 4);
            }
            fix1html = fix1html.Replace("[TIME]", time.ToString(format));
            File.WriteAllText("fix4test.html", fix1html);

            //testDetect();

        }//end test all



    }//end program class

}//end program class

