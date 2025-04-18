# SHARON Web Application

The **SHARON Web Application** (Simple Holistic Asset Record Online Navigator) is a centralized portal designed to streamline access to information on Remedy incidents, assets, changes, work orders and other IT resources. Built using ASP.NET Web Forms, SHARON provides a user-friendly interface for querying and managing IT data in a structured and efficient manner.

## Features

- **Centralized Information Access:**
  - Query and display data related to Remedy incidents, assets, changes, and more.
  - Consolidate IT resource information into a single, easy-to-navigate portal.

- **User-Friendly Interface:**
  - Responsive design with integrated CSS and JavaScript for enhanced usability.
  - Dedicated pages for different data types, such as incidents and assets.

- **Customizable Configuration:**
  - Flexible configuration options through `Web.config` and environment-specific overrides (`Web.Debug.config`, `Web.Release.config`).

## Project Structure

### Key Directories

- **`Sharon/`**: Contains the main ASP.NET Web Forms application files.
  - **`.aspx` Pages**: Web pages for displaying and interacting with data (e.g., `Index.aspx`, `Tickets.aspx`, `Assets.aspx`).
  - **Code-Behind Files**: Logic for handling page events and interactions (e.g., `Index.aspx.cs`, `Tickets.aspx.cs`).
  - **Designer Files**: Auto-generated files for maintaining control definitions (e.g., `Index.aspx.designer.cs`).
  - **Static Assets**:
    - **`css/`**: Stylesheets for the application.
    - **`js/`**: JavaScript files for interactivity.
    - **`img/`**: Images used in the application.
    - **`fonts/`**: Font files for custom typography.
  - **`bin/`**: Compiled binaries and dependencies.
  - **`Properties/`**: Project metadata and settings.

- **`Sharon.sln`**: The Visual Studio solution file for the project.

### Configuration Files

- **`Web.config`**: Main configuration file for the application.
- **`Web.Debug.config`**: Debug-specific configuration overrides.
- **`Web.Release.config`**: Release-specific configuration overrides.

## Prerequisites

### Software Requirements

- **.NET Framework**: Ensure the required version is installed.
- **IIS (Internet Information Services)**: For hosting the application.
- **Visual Studio**: For development and debugging.

### Configuration

- Update any hardcoded paths in the code-behind files or configuration files if necessary.
- Ensure all required dependencies are available in the `bin/` directory.

## How to Build and Run

1. Open the solution file `Sharon.sln` in Visual Studio.
2. Build the project to generate the necessary binaries.
3. Deploy the web application to IIS or run it locally using Visual Studio's built-in web server.
4. Access the application via the configured URL (e.g., `http://localhost:5000/`).

## Usage

1. Navigate to the homepage (`Index.aspx`).
2. Use the provided navigation to access specific data views:
   - Remedy tickets (`Tickets.aspx`)
   - Asset information (`Assets.aspx`)
   - Change records and more.
3. Interact with the UI to query and manage data.

## Security Considerations

- Implement proper authentication and authorization to restrict access to sensitive pages.
- Validate all user inputs to prevent injection attacks.
- Regularly review and update dependencies to address security vulnerabilities.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes and submit a pull request.

## Author

This solution was created by Kevin Olanday to provide a centralized portal for IT resource management and tailored to meet specific organizational requirements.

## Note on Modernization

While this project is functional, consider modernizing it for improved performance and maintainability:
- **Frontend**: Use modern frameworks like [React](https://reactjs.org/) or [Angular](https://angular.io/) for a dynamic UI.
- **Backend**: Migrate to [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/) for cross-platform support and better performance.
- **Deployment**: Leverage [Docker](https://www.docker.com/) and [Kubernetes](https://kubernetes.io/) for scalable deployments.
- **Authentication**: Implement [OAuth 2.0](https://oauth.net/2/) or [OpenID Connect](https://openid.net/connect/) for secure authentication.

## License

This project is proprietary and intended for internal use only. Contact the project owner for licensing details.