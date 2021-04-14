# Flight Simulator App

This project is of a flight simulator diagnosis, with the Flight-Gear program as a projector. The project is written in C# and follows the MVVM Design Pattern.

**Main Application Window:**
![alt text](https://github.com/giladaube/FlightSimulatorApp/blob/main/homescreen.png)

**View -** the display is separated into smaller components of views; each individual view holds a View-Model and has its own logic. Command and data binding, if exist, are to the View Model. To have a more professional fill-like we added a setup window to initialize the app. Some of the user stories are opened with side menu (hamburger).  
**View Model -** the View-Model receives the model provided in the main. To that model the View-Model sends commands and receive notification from.  
**Model -** for the Model we have decided to have a façade that holds Models so the View-Models will know only it. Even though the View-Models are separated we have decided to hold one model for there are some methods that intertwines. More so, the Models need shared data and so each model will know only itself the façade is holding the shared data.  
Model Player- the model player opens the Flight Gear app, connecting a TCP client to the app and sends data. The user may change the data that it sends (changing pace, exact time etc.) and by chain of command from the View to the View-Model to the Model the model will change the data that is sends accordingly.  
**Model Graphs -** the model saves the correlated features in the csv, displays graphs on command from its VM to the user chosen features, and the model calculate the regression line to display.  
**Model Anomalies -** the model calculates the anomalies based on a “normal” flight, the anomalies in each two correlated features may vary from algorithm to algorithm, therefore the user can upload a DLL of his choice to the app to calculate the anomalies based on the chosen library.  

**Graphs Window:**
![alt text](https://github.com/giladaube/FlightSimulatorApp/blob/main/Graphs.png)


Two built-in DLLs are the Linear Regression and the Minimum Enclosing Circle DLLs. They are located in the Resources folder, and are available for use during runtime.  
The Linear Regression DLL learns the linear regression line of every two correlated features from a "normal flight", and when analyzing an anomalous flight, it detects for every two correlated features which points (If there are any) are too distant from the linear regression line. The maximum distance allowed is based on the farthest point in the "normal" flight data. Points that are too far are colored in red and are considered as anomalous.  
The Minimum Enclosing Circle DLL learns from the "normal" flight data, for every two correlated features, the minimum circle that encloses all of the points created by them. When analyzing the anomalous flight, it checks for every two correlated features if there are any points that are located outside of the circle learned earlier. These points will be considered as anomalous.  
For every DLL, there is an option to jump to the relevant point of time easily, at the bottom-left corner.

**Anomaly Detection DLLs - Linear Regression and Minimum Enclosing Circle:**
![alt text](https://github.com/giladaube/FlightSimulatorApp/blob/main/Anomalies.png)

**The DLL requirements are as follows:**  
First, your DLL must be written in C#. Your main class should be called "Algorithm", and the DLL's filename must be the same as the namespace used.  

**Your DLL must have the following methods:**  

**Constructor** - The Constructor should receive a FlightGear XML file path, which has the flight features in it.  
**LearnNormal** - This method receives a normal flight CSV file path. This is where your dll should do the learning process and set boundaries and thresholds for the anomalies to later be analyzed.  
**Detect** - This method receives an anomalous flight CSV file path. In this method, you are recommended to check for every two correlated feature if there are any anomalous points that surpassed the threshold, and keep them in a data structure such as a list, to later be drawn on the graph differently from the non-anomalous points.  
**DrawGraph** - This method receives nothing and returns a PlotModel object, which is a part of the OxyPlot library. This is an object on which you can draw your graph according to your algorithm. More details about the OxyPlot library: https://oxyplot.github.io/  
**GetAnomaliesLinesteps** - This method receives a flight feature, and returns a List of ints, which represents rows in the CSV file, in which there were any anomalies.

**For your convenience, here are the methods signatures, to copy easily:**

**Constructor:** public Algorithm(string xmlPath)
**Methods:**
void LearnNormal(string csvPath, double minCorrelation);  
void Detect(string AnomalyCSV);  
List<int> GetAnomaliesLinesteps(string feature);  
PlotModel DrawGraph(string feature);


The project holds three main folders, in corresponding to the MVVM design pattern. Model folder, View-Model folder, and View folder.
 	Model Folder- holds the models and the model façades. And the classes the model uses.
	View-Model Folder- holds four View-Models that hold as members IModel that the Model facade is extending.
	View Folder- holds all the view components, each user story there is at least one view. Each view holds as a member a matched View-Model.

**Required Installments:**  
Flight Gear
NuGet Packages: MahApps.Metro, MahApp.Metro.IconsPacks, OxyPlot.Core
.NET framework 4.7.2
First installment: direction provided in setup window

You can find a Demo here: https://github.com/giladaube/FlightSimulatorApp/blob/main/DEMO.mp4

**UML Diagrams:**

![alt text](https://github.com/giladaube/FlightSimulatorApp/blob/main/UML%20-%20only%20classes.jpg)

With some more details:
![alt text](https://github.com/giladaube/FlightSimulatorApp/blob/main/UML%20-details.png)
