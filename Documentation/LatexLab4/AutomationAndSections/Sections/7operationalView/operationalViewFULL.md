Here's the plan:

    Create a top-level header for "The Operational Viewpoint".
    Extract and organize the "Definition", "Concerns", "Models", "Problems and Pitfalls", "Stakeholders", and "Applicability" sections as bullet points under a "Overview" heading.
    For "Concerns", create sub-sections for each listed concern: "Installation and Upgrade", "Functional Migration", "Data Migration", "Operational Monitoring and Control", and "Alerting".
    Populate each concern's sub-section with detailed explanations from the text, using bullet points for lists and preserving specific examples and terminology.
    Identify and label any incomplete sections or vague statements.# The Operational Viewpoint

Overview

    Definition: Describes how the system will be operated, administered, and supported when it is running in its production environment.
    Concerns:
        Installation and upgrade
        Functional migration
        Data migration
        Operational monitoring and control
        Alerting
        Configuration management
        Performance monitoring
        Support
        Backup and restore
        Operation in third-party environments
    Models:
        Installation models
        Migration models
        Configuration management models
        Administration models
        Support models
    Problems and Pitfalls:
        Lack of engagement with operational staff
        Lack of backout planning
        Lack of migration planning
        Insufficient migration window
        Missing management tools
        Production environment constraints
        Lack of integration into the production environment
        Inadequate backup models
        Unsuitable alerting
    Stakeholders:
        System administrators
        Production engineers
        Developers
        Testers
        Communicators
        Assessors
    Applicability: Any system being deployed into a complex or critical operational environment.

General Considerations

    Aims to identify a system-wide strategy for addressing operational concerns and finding solutions.
    For large information systems, focuses on ensuring reliability and effectiveness within the enterprise's IT environment (internal or external hosting).
    For product development, illustrates generic operational concerns customers might encounter and identifies solutions for product implementation.
    Often the least well-defined view, requiring refinement during construction as details emerge.

Concerns
Installation and Upgrade

    Scope: Ranges from development team installing on customer hardware, to ultimate users performing self-installation/integration, to allocating cloud resources and uploading software.
    Team Involvement: Often performed by a separate, authorized team expecting preplanned, largely automated processes.
    Types:
        Pure Installation: Initial deployment.
        Upgrade: Installing a current version when a previous version is already present. This is significantly more complex due to the need to respect existing data, configuration settings, state of running elements, and potentially keeping the system in operation.
    Architectural Concern: Focuses on ensuring the system can be installed or upgraded acceptably to stakeholders, involving technical specialists, developers, and production engineers.

Functional Migration

    Process: Replacing existing capabilities with the new system's offerings, typically migrating users from an older system.
    Approaches:
        Big Bang: Migration occurs in a single step at one point in time (e.g., over a weekend).
            Pros: Can be cheapest due to no resource replication.
            Cons: Extremely risky with no easy recovery route if migration fails.
        Parallel Run: New and old system versions run side-by-side until confidence in the new system allows switching off the old.
            Pros: Reduces risk.
            Cons: More expensive due to resource duplication and need for processes to synchronize systems.
        Staged Migration: Parts of a process or organization are moved to the new system incrementally to manage risk and cost.
    Key Issues: Risk and Cost.

Data Migration

    Process: Loading data from existing systems into new ones.
    Goal: Automate as much as possible, especially for large data volumes.
    Complexity Factors:
        Very old, variable quality, or poorly modeled existing data.
        Migration between geographical locations (security, performance concerns).
        Massive data stores (hundreds of gigabytes/terabytes) are more likely to contain non-conforming data requiring exceptional/manual processing.
        Time required: Can take days/weeks for extraction, sorting, loading, and index creation for large datasets.
    Software Nature: Typically viewed as utility software with a limited life, not requiring long-term support. May comprise automated software, semiautomated procedures, and manual intervention for exceptions.
    Tools:
        ETL (Extraction, Transformation, Load) tools: Help automate the process, allow visual definition of transformation rules, access various physical formats, perform standard transformations, and monitor results.
        Database replication facilities: Useful for data migration and keeping databases synchronized over time.
    Challenge with Live Systems: Migrating data from a live system that continues to be updated requires special handling (e.g., capturing and applying updates to the new system after bulk migration).
        Example: Government tax office database migration: A 2-week migration window where the source system continues to receive 100,000 updates requires special code to capture and apply these updates to the new system.
    Management: Should be managed as a development project with requirements, design, build and test, and acceptance phases. Architectural principles apply, but success criteria focus on successfully migrated data rather than the migration software itself.

Operational Monitoring and Control

    Purpose: Routine monitoring to ensure correct system function and routine control operations (startup, shutdown, transaction resubmission, etc.).
    Variability:
        Some systems need little monitoring (e.g., file server).
        Others need extensive monitoring (e.g., large financial reconciliation system to identify/rectify communication link and data reconciliation failures).
    Dependency: Amount of monitoring/control depends on the likely number and variety of unexpected operational conditions.
    Trade-offs: Development and integration of monitoring/control facilities can be a major effort, requiring balancing stakeholder needs against cost and time.
    Environment Consideration: Solutions must be appropriate for the system's deployment environment.

Alerting

    Definition: Notification from the system that an event has occurred, typically something requiring human intervention.
    Types of Alerts:
        Technical: E.g., system unable to connect to a database server.
        Business: E.g., bad data received on an automated input.
        Non-failure Events: Significant events like service startup or shutdown (for information).
    Function: Active system function; alerts sent to a central console or alert management tool for display to support staff.
    Action Triggered: Server restart/reset, batch job resubmission, handover to development for diagnosis/repair.
    Standards: Many organizations have corporate standards for alerting (e.g., events to alert, information to include, alert destination, advice to avoid alert flooding).
    Third-Party Hosting: Hosting providers almost certainly have their own proprietary mechanisms for raising and monitoring alerts.

    Configuration Management
Purpose: Manage configuration parameters of deployment environment elements (databases, operating systems, middleware, and software) to ensure system reliability and predictability.
Key Points:
Configuration management addresses the complexity of managing multiple configurations and reduces operational risk.
Involves grouping, modifying, and tracking configuration parameters.
Typically handled by system administration and production engineering teams.
Architectural considerations:
Understand operational configuration requirements.
Ensure configurations are achievable and acceptable to stakeholders.
Performance Monitoring
Purpose: Measure and improve system performance as part of performance engineering.
Key Points:
System must capture, present, and store accurate performance metrics.
Production system administrators play a key role in identifying and responding to performance issues.
Early involvement of administrators ensures compatibility with proposed solutions.
Metrics and reporting details are discussed further in Chapter 26.
Support
Purpose: Define the type and level of support required for the system and associated hardware.
Key Points:
Support considerations include end users, support staff, and maintainers.
Channels for delivering support must be clearly defined.
Backup and Restore
Purpose: Protect and ensure recoverability of valuable organizational data.
Key Points:
Backup processes must be carefully designed, executed, and regularly tested.
Restore processes must ensure transactional consistency of data.
Challenges:
Distributed data complicates consistency and recovery.
Strategies may include manual or automated data recovery or reverting the system to a previous state.
Example Scenarios:
Failure to capture backup logs leading to data loss.
Distributed databases requiring coordinated recovery to maintain consistency.
Considerations for distributed systems:
Ensure transactional consistency across all data stores.
Plan for scenarios involving partial data corruption or loss.
Operation in Third-Party Environments
Purpose: Address operational considerations for external hosting environments (e.g., cloud computing).
Key Points:
Benefits: Simplicity, flexibility, and cost-effectiveness.
Challenges:
Integration with third-party monitoring, alerting, and management tools.
Data migration into and out of the environment.
Remote-only operational actions (no physical server access).
Understanding and testing third-party backup and restore facilities.
Early planning is critical to address these challenges effectively.
Stakeholder Concerns
Stakeholder Classes and Concerns:
Assessors: Functional migration, data migration, support, and operation in third-party environments.
Communicators: Installation, upgrade, functional migration, operational monitoring, and third-party operations.
Developers: Operational monitoring, performance monitoring, and third-party operations.
Production Engineers: Installation, upgrade, configuration management, performance monitoring, and third-party operations.
Support Staff: Functional migration, data migration, alerting, support, and third-party operations.
System Administrators: All concerns.
Testers: Installation, upgrade, functional migration, data migration, monitoring, and third-party operations.
Users: Support.
Models
Purpose: Illustrate how the system will be deployed and maintained in production.
Key Points:
Models can be large and detailed; summaries in the architecture document (AD) should reference fuller models elsewhere.
Installation Models:
Demonstrate practical installation and upgrade processes.
Address requirements and constraints imposed by the architecture.
Include:
Elements to be installed or upgraded.
Dependencies between installation items.
Constraints on installation/upgrade processes.
Rollback strategies for failed installations/upgrades.
Notation:
Text and tables for simple cases.
Dependency diagrams for complex dependencies.
Activities:
Identify installation groups and their elements.
Define technical dependencies and constraints.
Plan the overall installation process.
Incomplete Sections or Clarifications Needed
Backup and Restore:
Specific strategies for distributed systems need elaboration.
Clarify how transactional consistency is maintained across distributed data stores.
Operation in Third-Party Environments:
Details on integrating with specific third-party tools and services.
Examples of remote operational actions.
Installation Models:
More detailed examples of dependency diagrams and rollback strategies.

Operational View: Installation Model

This section details the installation process for the system, focusing on groups, dependencies, constraints, and backout strategies.
Installation Groups

    Windows Desktop Client:
        Content: All software in the WIN-CLIENT component.
        Installation Method: InstallShield automatic installer, remotely executed via the management tool.

    Database Schema:
        Content: All DBMS schema definitions and data abstraction stored procedures.
        Packaging: Simple SQL scripts.
        Installation Method: Custom-written Perl script.

    Web Interface:
        Content: Server-resident user interface components (the WEBINTERFACE component).
        Installation Method: Manual administrative action, copying files into IIS directories according to written instructions.

    Rental-Tracking Service:
        Content: .NET assemblies implementing services called by the Web and Windows interfaces (the RENTALTRACKER component).
        Installation Method: Manual administrative action, copying files into IIS directories according to written instructions.

    Reporting Engine:
        Content: .NET assemblies implementing the summary reporting engine.
        Installation Method: Manual administrative action, copying files into IIS directories according to written instructions.

Dependencies

    Windows Desktop Client, Web Interface, Rental-Tracking Service, and Reporting Engine depend on Database Schema.
    Windows Desktop Client and Web Interface depend on Rental-Tracking Service.
    Web Interface depends on Reporting Engine.

Constraints

    Windows Desktop Client: A restart of the client machine will be required during this installation process.

Backout Strategy

This is the first release; backout involves uninstallation.

    Windows Desktop Client: Run the installer with an uninstall flag.
    Database Schema: A custom Perl script will be supplied to remove all objects created during the installation.
    Web Interface: Manual administrative action required; supplied instructions will list files to be removed.
    Rental-Tracking Service: Manual administrative action required; supplied instructions will list files to be removed.
    Reporting Engine: Manual administrative action required; supplied instructions will list files to be removed.

Operational View: Migration Models

This section outlines strategies for migrating information and users to the system, populating the new system, synchronizing data, and potential backout to the old system.
Purpose

To define the strategies for:

    Migrating information and users.
    Populating the new system with information from the existing environment.
    Synchronizing information between new and old environments (if required).
    Reverting to the old system if serious problems arise with the new one.

Focus

The migration model focuses on requirements and constraints imposed by the current architecture on the detailed migration process.
Notation

    Usually documented using text and tables.
    Informal diagrams may illustrate data migration and synchronization.
    Complex data migration may require data modeling notation (e.g., entity-relationship diagrams) to illustrate transformations.

Activities

    Establish Possible Strategies: Assess architecture and existing systems to identify possible migration strategies (e.g., big bang, parallel run, staged migration), their workings, and tradeoffs.
    Define the Primary Strategy:
        May involve defining options for others to decide (e.g., for a product with different customer migration needs).
        May involve defining the best strategy for stakeholders, minimizing business disruption, especially for longer migration periods.
    Design the Data Migration Approach:
        Choose an appropriate approach for populating the system with existing information.
        Estimate duration, tasks, and resources required.
    Design the Information Synchronization Approach:
        Identify an overall approach for synchronizing information between old and new systems, particularly for parallel run strategies.
        Determine if synchronization is unidirectional (new system only) or bidirectional.
    Identify the Backout Strategy:
        Decide if a backout strategy to the existing system is required.
        Determine how such a backout would work, considering potential impracticalities like reverse data migration.

Operational View: Configuration Management Models

This section describes how the system's configuration is managed, including item grouping, dependencies, value sets, and change strategies.
Purpose

To explain:

    Groups of configuration items and their management.
    Dependencies among configuration groups.
    Different configuration value sets for routine operation and their purpose.
    Application of configuration values to the system, considering operational environment characteristics.

Evolution

This model is unlikely to be complete in early stages of the Architecture Document (AD) but can be elaborated as system construction progresses.
Notation

    Often simple, documented with text and tables.
    In complex cases, primarily treated as a data model, using notations like entity-relationship diagrams or UML.

Activities

    Identify the Configuration Groups:
        Group configuration values into cohesive units with minimal inter-group dependencies.
        Name each group, explain its purpose, and describe its management (how values are defined, collected, applied).
    Identify Any Configuration Group Dependencies:
        Clearly identify and record dependencies among configuration groups (e.g., DBMS parameter changes impacting operating system reconfiguration, or adding instances affecting application server configuration).
    Identify Configuration Value Sets:
        Determine the number of configurations needed during the system's routine operational lifecycle.
        Define characteristics of each value set and identify configuration groups that change between different configurations.
        Define the purpose of each set and when it needs to be applied.
    Design the Configuration Change Strategy:
        Design a practical overall approach for applying configuration changes in the production environment, considering constraints.

Example: Rental-Tracking System Configuration Management Model
Configuration Groups

    DBMS Parameters:
        Content: SQL Server 2008 parameters controlling initialization, operation, and performance of the database.
        Management: Via SQL scripts, applied by database administrators.
    IIS Parameters:
        Content: IIS parameters controlling initialization, operation, and performance characteristics of the server.
        Management: Using a set of PowerShell scripts supplied with the system.
    Reporting Engine Options:
        Content: Reporting Engine parameters controlling what is summarized and when summaries are computed.
        Management: As a set of configuration files read by the component.

Configuration Dependencies

    When IIS parameters are set to allow more connections, the DBMS parameters must be changed to allow for the possible increase in load.
    If the Reporting Engine Options are set for more aggressive summary activity, the DBMS parameters must be set to allow for an increased amount of data cache being required.

    Operational View

This section defines the operational requirements and constraints of the architecture and the facilities it provides for administrative users.
Configuration Sets

    Standard:
        Workload: Up to 1,200 concurrent users.
        Reporting Engine: Produces level 1 summary statistics every 6 hours.
    High Volume:
        Workload: Increases capacity to 2,000 concurrent users.
        Reporting Engine: Routine operation switched off.
    Month End:
        Timing: Applied during the last two days of the month.
        Workload: Limits concurrent usage to 800 users.
        Reporting Engine: Runs continually to produce complete summary statistics.

Configuration Change Strategy

The configuration sets will be applied sequentially:

    DBMS Reconfiguration:
        Performed by: Database administrator.
        Method: Running a single script to set parameters for the desired configuration set.
        Impact: Could involve a DBMS restart.
    Reporting Engine Options Change:
        Method: Altering the Engine's configuration file parameter.
        Impact: Requires restarting the Reporting Engine.
    IIS Configuration Application:
        Performed by: Administrator.
        Method: Running the appropriate PowerShell script.
        Impact: Requires restarting the IIS server.

Administration Models

The administration model must define the following items:

    Monitoring and Control Facilities:
        Purpose: Support system administrators.
        Scope: May involve custom utilities, features, and/or integration into existing internal or third-party management environments.
        Examples: Basic message log to full-blown integration with management/monitoring infrastructure.
        Definition Requirement: Clearly define facilities provided, used, or integrated; how they address the problem; and any limitations.
    Required Routine Procedures:
        Purpose: Identify administrative work performed regularly or in exceptional circumstances.
        Examples: Weekly backup, monthly health check, or complex 24/7 procedures for high-volume systems.
        Definition Requirement: For each procedure, define its purpose, when it is performed, who performs it, and what is involved. Cross-reference relevant monitoring and control facilities.
    Likely Error Conditions:
        Purpose: Explain unique error conditions related to the architecture that require administrative intervention.
        Exclusions: Does not cover underlying platform failures for which administrators are typically already experts.
        Definition Requirement: Include when the condition can occur, how to recognize it (referencing monitoring facilities), how to rectify it (referencing control facilities), and possible further failures it could trigger.
    Performance Monitoring Facilities:
        Purpose: Enable monitoring of system performance.
        Distinction from Operational Monitoring: Operational monitoring reports by exception; performance monitoring extracts and analyzes data routinely to track performance over time.
        Definition Requirement: Explain types of performance measures available and how administrators or developers will extract and analyze information.
        Cross-Reference: Strong cross-reference between administrative facilities in this model and the common design model in the Development view. Operational view defines facilities for administrative stakeholders; Development view defines common processing to achieve those facilities.

Notation

    Primary Customers: System administrators (may not be software developers).
    Recommended Notation: Nearly always text and tables, augmented with a few informal diagrams where needed.
    Less Appropriate: Extensive use of more formal notation such as UML.

Activities

    Identify the Routine Maintenance Required:
        Task: List operational tasks to keep the system running smoothly in production.
        Details: For each task, define who performs it, when, and how.
    Identify Your Likely Error Conditions:
        Task: Analyze the architecture using primary usage scenarios to identify potential failures during the operational lifecycle (e.g., element failures, data store filling, out of memory).
        Focus: Include administration and maintenance-related failures, not just end-user impacting ones.
        Details: Identify classes of error conditions, causes, rectification methods, and estimated availability impact. Consider conditions if routine maintenance isn't performed.
    Specify Any Custom Utilities:
        Task: Determine if system-specific utilities are required for routine and exceptional procedures.
        Scope: Can range from simple database/OS scripts to significant software.
        Details: Specify any required utilities.
    Identify the Key Performance Scenarios:
        Task: Extract critical performance-related scenarios from overall system usage scenarios (e.g., time-critical, high workload, frequent execution, key stakeholder requirements).
    Identify the Performance Metrics:
        Task: For key performance scenarios, identify metrics to measure performance and analyze resource consumption.
        Approach: Identify classes of metrics rather than individual ones for abstraction.
        Details: Record what each metric or class means and its use.
    Design the Monitoring Facilities:
        Task: Design outline-level monitoring facilities for routine system monitoring, error condition recognition, and performance metric gathering.
        Scope: To be fleshed out during development increments.
        Details: Provide enough detail to clarify what needs to be done in each system element to provide administration facilities.

Example (Rental-Tracking System)
Monitoring and Control

    Server Message Logging: All server components write information, warning, and error messages to the Windows Event Log of the machine they are running on.
    Client Message Logging: Client software logs messages on unexpected errors to the hard disk of the client machine for manual retrieval.
    Startup and Shutdown: No system-specific facilities; IIS and SQL Server facilities are considered sufficient as software runs within their context.

Operational Procedures

    Backup:
        Scope: Operational data in SQL Server database.
        Frequency: Transaction logs backed up every 15 minutes; application databases backed up daily.
        Responsibility: Details left to database administrators.
    Pruning of Summary Information:
        Issue: Reporting Engine does not remove summary information.
        Action: Database administrators monitor Reporting Engine and Windows client reporting performance and manually prune summary information when volume impacts performance.
        Support: A written procedure will be supplied.

Error Conditions

    Database Out of Log Space:
        Cause: Transaction volume rises, filling transaction log.
        Impact: System suspends operation.
        Rectification: Database administrators recognize problem, manually back up logs to free space. If routine, reduce transaction log backup interval.
    Database Out of Data Space:
        Cause: Database runs out of data space.
        Impact: System stops operating.
        Rectification: Database administrators recognize condition and either prune summary information (see above) or add more data space.
        Support: Written estimate of space required for various workloads will be provided.
    IIS Failure:
        Cause: IIS server fails.
        Impact: System completely fails, Windows clients lose contact with server.
        Rectification: Administrators recognize condition and restart IIS. System recovers automatically; Windows clients automatically reconnect.

Performance Monitoring

    Application-Specific: No application-specific facilities planned.
    Approach: Use existing facilities.
    Facilities:
        SQL Server Counters: SQL Server 2008 allows collection and viewing of performance counters via Windows Server 2008's Reliability & Performance Monitor and SSMS Activity Monitor.
        Usage: These metrics should be used to assess database workload volume and time spent.

Operational View: Support Models

This section details the support model for the system, outlining who provides support, who receives it, and how incidents are handled and escalated. The focus is on providing a strategic overview rather than detailed procedures.
Performance Counters

    IIS/ASP.NET Counters:
        Collected via Windows Server 2008's Reliability & Performance Monitor.
        Used to assess the number of Web requests serviced and their service time.
    .NET Counters:
        Collected via Windows Server 2008's Reliability & Performance Monitor.
        Used to establish the amount of non-Web-request workload and its completion time.

Support Model Definition

The support model defines:

    Groups needing support: Clearly defines stakeholder groups requiring support, the nature of support needed, and delivery mechanisms.
    Classes of incidents: Defines incident types likely to be encountered and expected response times, characterized by operational, organizational, or financial impacts.
    Support providers and responsibilities: Identifies support providers and their responsibilities for incident resolution.
    Escalation process: Defines how serious incidents are escalated between support providers and their responsibilities during escalation.

Notation:
This model should primarily be text-and-tables, with flow diagrams (e.g., UML activity diagrams) used to clarify information flow and decision-making processes. It needs to be understandable by both technical and non-technical stakeholders.
Support Model Activities

The following activities are involved in defining the support model:

    Identify the Supported Groups: Define stakeholder groups, their support needs, and potential support avenues.
    Identify the Support Providers: Determine who will provide support, defining their responsibilities and how support is to be delivered.
    Identify Any Incidents Requiring Support: Characterize incident types that could trigger support needs, including likely frequency and severity.
    Map the Providers, Incidents, and Groups: Assign support providers to incident types for specific stakeholder groups, ensuring suitable support is offered.
    Plan the Escalation: Define escalation paths between internal and external support providers and their responsibilities during escalation.

Example Support Model for Rental-Tracking System
Supported Groups

    Web Users:
        Need support for site problems or difficulties using the Web interface.
        Assumptions: Few can be made about this group.
        Primary support channel: E-mail, with telephone backup.
    Windows Users:
        Internal users of the Windows client.
        Need help with usage issues, system problems, and PC support.
        Primary support channel: Telephone, with willingness to receive support via e-mail.
    Windows Administrators:
        Technically sophisticated administrators of server machines.
        Need assistance only in unexpected failure scenarios.
        Need immediate assistance via telephone and query resolution via e-mail.
    Database Administrators:
        Technically sophisticated.
        Need assistance only with unfamiliar database behavior.
        Need immediate assistance via telephone and query resolution via e-mail.

Support Providers

    Web Services Help Desk:
        Organizational group responsible for resolving all support incidents raised by Web interface users.
        Provides support via e-mail and telephone, six days per week, 20 hours per day.
    IT Help Desk:
        Organizational group responsible for resolving all support incidents raised by Windows client interface users.
        Provides support via e-mail and telephone, often with direct assistance at the end user's desk.
        Support provided during normal business hours.
    DBA Group:
        Organizational group responsible for resolving all support incidents related to database management systems.
        Provides support via e-mail and telephone.
        Support normally provided during normal business hours, with on-call staff option outside this period.
    Windows Administrators:
        Organizational group responsible for resolving all support incidents related to IIS, .NET, Windows Server 2008, and underlying hardware.
        Provides support via e-mail and telephone.
        Support provided during normal business hours, with on-call staff option outside this period.
    Microsoft Support:
        External organization (Microsoft Corporation’s Support division).
        Responsible for assisting with resolution of incidents caused by faults or usage problems with SQL Server 2008, Windows Server 2008, or IIS products.
        Provides support via e-mail, newsgroups, Web sites, fax, and telephone.
        Support provided 24 hours per day, every day.
    Development Team:
        Organizational group that developed and maintains the system.
        Responsible for resolving any incident other support providers cannot resolve.
        Provides support via e-mail, telephone, and site visits during normal business hours, with ability to reach on-call staff during other times.

Support Incidents and Resolution

    Web Usage Difficulties:
        Description: User problems with Web interface not caused by system component failure.
        Resolution: Single interaction with Web Services Help Desk (phone or e-mail).
        Impact: Minimal.
    Windows Usage Difficulties:
        Description: User problems with Windows client interface not caused by system component failure.
        Resolution: Single interaction with IT Help Desk (phone or e-mail).
        Impact: Minimal.
    End-User System Errors:
        Description: User encounters problem caused by system component failure or malfunction.
        Resolution: Within 1 working day. User interacts solely with IT or Web Services Help Desk, who manage resolution and engage other providers.
        Impact: Moderate, should not threaten business operations beyond inconvenience.
    Slow End-User Performance:
        Description: End users complain of unacceptably slow performance.
        Resolution: Within three working days. User interacts solely with IT or Web Services Help Desk, who manage resolution and engage other providers.
        Impact: Moderate, should not threaten business operations beyond inconvenience.
    Database Corruption:
        Description: Database system reports internal corruption.
        Resolution: Within 2 hours (original incident resolution). DBA Group responsible.
        Impact: Moderate, business operations interrupted.
    Database Failure:
        Description: Database system needs recovery from backups.
        Resolution: Within 4 hours. DBA Group responsible.
        Impact: Severe, business operations interrupted for incident duration.
    IIS or Windows Server Failure:
        Description: IIS Server, underlying OS, or hardware failure.
        Resolution: Within 1 hour. Windows Administrators responsible.
        Impact: Severe, business operations interrupted for incident duration.

Escalation Process

The escalation process is as follows:

    Web interface users report problems to the Web Services Help Desk.
    Windows client interface users report problems to the IT Help Desk.
    Help Desks report system problems to the Windows Administrators.
    Windows Administrators report database problems to the DBA Group.
    Windows Administrators report other problems to the Development Team.
    Windows Administrators, DBA Group, and Development Team report problems with Microsoft software to the Microsoft Support organization.

Operational View

This section details common problems and pitfalls related to the operational aspects of a software system, along with suggested risk reduction strategies. It also provides a checklist for operational readiness and references for further reading.
Problems and Pitfalls
Lack of Engagement with the Operational Staff

    Problem: A disconnect often exists between development staff (builders) and operational staff (deployers and administrators), hindering smooth, incident-free system rollouts.
    Risk Reduction:
        Engage operational groups early, emphasizing the value of their contribution.
        Use an explicit Operational view to address their requirements.

Lack of Backout Planning

    Problem: Many systems lack real backout plans, and commercial products often lack graceful recovery mechanisms for failed upgrades. This leads to reliance on a perfect rollout.
    Risk Reduction:
        Ensure the system can be backed out of its production environment by defining and reviewing a clear procedure.

Lack of Migration Planning

    Problem: Systems replacing existing ones (manual, automated, or earlier versions) are often developed without a comprehensive migration plan, impeding smooth deployment.
    Risk Reduction:
        Understand the architecture's migration needs as early as possible.
        Address migration needs within the Architecture Document (AD).

Insufficient Migration Window

    Problem: Data migration consistently takes longer than anticipated due to data quality issues, inconsistencies, and challenges with large data volumes. This is exacerbated when moving data across geographical locations or different data store types.
    Risk Reduction:
        Plan for handling data errors and inconsistencies.
        Develop and gain stakeholder buy-in for processes accepting migrated data.
        Factor in storage requirements for transitional data in hardware sizing models.
        Include adequate elapsed-time contingency in the migration plan.
        Factor in time for database reorganization, index creation, etc.
        For migrations from live systems with substantial migration times, create strategies for reconciling data updates made during the migration period.

Missing Management Tools

    Problem: Developers and architects often focus on building new software, neglecting operational facilities. This results in systems difficult to monitor and control without sophisticated tools, which operational staff often lack compared to developers' internal knowledge.
    Risk Reduction:
        Understand and involve administration stakeholders early in developing the Operational view.
        Ensure administrators' needs are met with standard, system-wide facilities.

Production Environment Constraints

    Problem: All production environments (in-house or external) impose constraints on application operation. Using mixed environments (e.g., in-house hosting with cloud "burst" capacity, or different cloud providers for production and disaster recovery) further complicates this. Constraints can include rigid unavailability periods, required tools, limited platform components, strict procedures, and SLA limitations, all impacting achievable system qualities.
    Risk Reduction:
        Agree on target production environment(s) early to understand their constraints.
        Clearly define required and expected qualities from a production environment (e.g., reliability, capacity, availability) to identify potential problems.
        Analyze planned production environments to understand opportunities and constraints and integrate them into the work.
        Obtain definite service level commitments from environment suppliers and test them for realism.

Lack of Integration into the Production Environment

    Problem: New systems frequently fail to integrate smoothly with existing production environments, forcing operational staff to learn new interfaces, tools, or management approaches, especially with third-party hosting.
    Risk Reduction:
        Understand the existing environment and its integration needs early in system design.
        Involve experts of the target production environment early for advice on its workings and required integration level.

Inadequate Backup Models

    Problem: Backup and restore processes can fail, leading to critical data loss if problems are only discovered during a recovery attempt.
    Risk Reduction:
        Do not neglect or omit this area.
        Incorporate backup and restore as a central part of the architecture, not an afterthought.
        Ensure the backup scheme includes all necessary information for data recovery.
        Estimate backup and recovery times and perform realistic testing.
        Describe how data will be restored, not just backed up.
        Consider maintaining data consistency across multiple data stores when one needs to be restored.
        Consider a "belt-and-braces" approach (e.g., writing updates to an audit/recovery area in addition to the main database to replay transactions).

Unsuitable Alerting

    Problem: Manifests as either "alert starvation" (system fails to send appropriate alerts for important events) or "alert flooding" (system sends excessive alerts, leading to important ones being missed or ignored). Both are significant operational problems, escalating small incidents into major ones.
    Risk Reduction:
        Although primarily a design/build concern, establish suitable architectural principles and approaches for alerting in the AD.

Checklist

    Installation:
        Do you know what it takes to install your system?
        Do you have a plan for backing out a failed installation?
        Can you upgrade an existing version of the system (if required)?
    Production Environment:
        Do you understand the facilities and constraints of the proposed production environment(s)?
        Can you live with or mitigate these if they are not ideal?
    Migration:
        Do you know how information will be moved from the existing environment into the new system?
        Do you have a clear migration strategy to move workload to the new system?
        Can you reverse the migration if needed?
        How will you deal with data synchronization (if required)?
        Is the data migration architecture compatible with the available time?
        Are there catch-up mechanisms in place where the source data is volatile during data migration?
    Backup and Recovery:
        Do you know how the system will be backed up?
        Are you confident the identified approach allows reliable system restoration in an acceptable time period?
    Monitoring and Control:
        Are administrators confident they can monitor and control the system in production?
        Do administrators have a clear understanding of the procedures they need to perform for the system?
        How will performance metrics be captured for the system’s elements?
    Configuration Management:
        Can you manage the configuration of all the system’s elements?
    Support:
        Do you know how support will be provided for the system?
        Is the support suitable for the stakeholders it is being provided for?
    Cross-Referencing:
        Have you cross-referenced the requirements of the administration model back to the Development view to ensure consistent implementation?

Further Reading

    Limited existing literature on operational aspects from an application development team's perspective.
    Books focusing on installing and managing specific technologies are abundant, but few examine principles underpinning reliable production systems.
    Partially address this area: [KERN04], [BEHR05], and [JAYA05].
    [ALLS10]: A collection of essays by operations experts offering insight into production operations.
    Dyson and Longshaw [DYSO04]: Includes patterns useful in the Operational view.
    ITIL: Influential model for understanding how production services are provided.
    [BON07]: Concise overview of ITIL v3.