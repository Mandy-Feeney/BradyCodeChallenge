# Brady Code Challenge

## Getting Started
This is a .Net 6 Console Application and requires .Net6 to compile and run.

Before running the application you need to set-up the input and output directories in the App.config file.  The input directory is where the json file containing order details will be placed and the output directory is where a summary of customer's orders will be placed.

When the Console application is running, place the orders json file in the input directory which was configured.  The Console application will monitor the input directory for any new .json files being created in there and will only detect files added to this directory after the app has started.

To see what errors there are when processing the input json file, these will be output to the Bin directory where the Console app gets ran from, in an Errors.json file.
