# 📚 Library Management System (WPF, C#)





###### 📌 Overview



This project is a simple desktop application built using **C#** and **WPF** that simulates a basic library management system. It allows users to manage books through a graphical user interface instead of a console-based approach.



The application demonstrates core concepts of *object-oriented programming*, *data binding*, and *desktop UI development*.





###### 🚀 Features



* ➕ Add new books (Title \& Author)
* 🗑️ Delete selected books
* 📖 Borrow books
* 🔄 Return books
* 🔍 Live search (filter by title or author)
* 📊 Sort books by clicking column headers
* 📋 Real-time UI updates using *ObservableCollection*





###### 🧠 Technologies Used



* **C#**
* **WPF (Windows Presentation Foundation)**
* **.NET**
* **OOP principles**
* **Data Binding**
* **INotifyPropertyChanged**





###### ⚙️ How It Works



* The *Library.cs* class manages all book operations (add, remove, borrow, return).
* The *Book.cs* class represents the data model and implements *INotifyPropertyChanged* for UI updates.
* The UI is built with WPF and uses a *DataGrid* to display books dynamically.
* *ObservableCollection* ensures that changes in the data are automatically reflected in the UI.





###### 🛠️ Prerequisites



* Visual Studio
* .NET SDK





###### 🎯 Future Improvements



* 🔐 User authentication
* 🗄️ Database integration (SQLite / SQL Server)
* 🧱 MVVM architecture
* 🌐 REST API integration
* 🎨 Improved UI/UX design



