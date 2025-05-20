The Development Viewpoint
Definition

Describes the architecture that supports the software development process.
Concerns

    Module Organization: Managing the logical structure of source code, including dependencies between modules, to ensure maintainability, buildability, and release management.
    Common Processing: Identifying and isolating reusable code modules for shared functionalities (e.g., logging, configuration).
    Standardization of Design: Establishing consistent design approaches using patterns and off-the-shelf software to enhance maintainability, reliability, and technical cohesion.
    Standardization of Testing: Defining consistent test approaches, technologies, and conventions, including test tools, infrastructure, data, and automation.
    Instrumentation: Implementing code for logging information (step execution, system state, resource usage) to aid monitoring and debugging. This capability should be controllable (switch off, alter detail level, or remove with build tools). Log destinations can include system console, files, or message services; metrics can be logged to files or databases.
    Codeline Organization: Managing the source code directory structure, configuration management, regular builds and testing (continuous integration), and release of tested binaries. This ensures reliable system delivery, especially with iterative development.

Models

    Module Structure Models:
        Defines the organization of the system's source code into modules and their dependencies.
        Often imposes higher-level organization on modules (e.g., layers) to manage dependencies.
        Layering rules can be defined to control inter-module dependencies (e.g., modules only communicate within their layer or adjacent layers, with exceptions for performance/efficiency).
        May require multiple models for distinct client/server elements, or be less useful for extensions to monolithic applications.
        Notation:
            UML component diagram: Package icon for code modules, dependency arrows for inter-module dependencies.
            Module grouping shown by enclosing stereotyped packages.
            Simple boxes-and-lines diagram: Shows layers, their ordering, and components within them.
        Example (Figure 20-1 description):
            Illustrates a three-layer module organization using stereotyped UML packages.
            System modules are UML packages within layers.
            Shows domain layer depends on utility layer, which depends on platform layer.
            Demonstrates nonstrict layering: domain-layer components directly access Java Standard Library (platform layer) instead of via utility components (in contrast, JDBC Driver is not directly accessed).
        Activities:
            Identify and Classify the Modules: Group source code into modules and optionally classify them (e.g., by abstraction) into higher-level organizations.
            Identify the Module Dependencies: Define clear dependencies between modules (or groups) to understand change impact.
            Identify the Layering Rules: Design rules for layered approaches, considering flexibility versus strictness for quality properties (e.g., performance).
    Common Design Models:
        Defines design constraints for system software elements to maximize commonality and reduce risk and duplication.
        Increases technical coherence, ease of understanding, operation, and maintenance.
        Components:
            Definition of common processing required across elements:
                Initialization and recovery
                Termination and restart of operation
                Message logging and instrumentation
                Internationalization
                Use of third-party libraries
                Processing configuration parameters (startup or runtime)
                Security (authentication, encryption)
                Transaction management
                Database interaction
                Internal and external interfacing

Problems and Pitfalls

    Too much detail
    Overburdened architectural description
    Uneven focus
    Lack of developer focus
    Lack of precision
    Problems with the specified environment

Stakeholders

    Production engineers: May be involved in or responsible for provisioning development/test environments, and controls over system transition to production.
    Software developers: Concerned with all aspects of this view.
    Testers: Concerned with common processing, instrumentation, test standardization, and possibly codeline organization.

Applicability

Relevant for all systems with significant software development involvement, from configuring off-the-shelf software to developing systems from scratch. The importance depends on system complexity, developer expertise, technology maturity, and team familiarity.
General Considerations

    Considerable planning and design of the software development environment is often required for complex systems.
    Focus on architecturally significant concerns to provide a stable environment for detailed design.

Chapter 20: The Development Viewpoint

This chapter focuses on defining common approaches for system elements, standardizing design, and establishing common software usage within the development view.
Common Design Model

The common design model is a partial design document that uses a combination of text and formal notation like UML. It aims to define:

    Common Processing:
        Identify and define common processing requirements across system elements. This contributes to the system's technical coherence.
        Example: Message Logging
            All components must log human-readable messages detailing occurrences and expected corrective actions.
            Messages must be logged at one of five levels: Fatal, Error, Warning, Information, Debug.
                Fatal: Unrecoverable error, component stops immediately.
                Error: Unrecoverable error, component can reset and continue.
                Warning: Possible error or unexpected condition requiring operator review.
                Information: Normal operation conditions, no operator intervention needed.
                Debug: Internal operational details of the component.
            Components should log messages at all five levels.
            Logging should use a standard library (defined later) for consistent destination, format, and configuration.
        Example: Internationalization
            All user- and administrator-visible strings must be stored in message catalogs; no hard-coded strings in source code.
            Parameters must be inserted into internationalized strings using position-independent placeholders to avoid ordering issues across languages.
            Locale-sensitive information (dates, times, currency) must be formatted according to the current locale; default formats should not be used.
            Strings for Debug level or internal use should not be internationalized and can be hard-coded.

    Standard Design Approaches:
        Define standard design approaches for implementing subsystems, especially where common processing or system-wide impact is anticipated.
        These act as specialized design patterns, defining what the approach is, where and why it should be used.
        Example: Internationalization
            Locale-sensitive resources (primarily strings) should use an external resource catalog. Strings must be extracted from the catalog before use.
            For Java server software, use Java Platform's native internationalization facilities: resource bundle, java.text formatting classes, and Locale class.
            The relationships between these internationalization elements should be defined.
            A design pattern for using Java internationalization facilities should be documented here. (Incomplete section: "You would place a definition of a design pattern for using the Java internationalization facilities here.")

    Common Software Usage:
        Define what common software should be used and how it should be used. This could be due to higher-level decisions (e.g., database access library) or reusable components (e.g., third-party logging library, local GUI element).
        Common elements, their usage locations, and methods must be clearly identified.
        Example: Message Logging
            All message logging must use the standard CCJLog package, part of the standard build environment.
            The CCJLog package must be used in a standard way, documented as a code sample in src/server/sample/logging/CCJLog source directory.

Activities for Common Design Model

    Identify Common Processing: Determine what common processing is required, its applicability (all or some elements), and how it should be performed.
    Identify the Required Design Constraints: Assess if common processing needs standardization or if critical subsystem design aspects will negatively impact the system if not designed in a certain way. Define and add relevant design constraints.
    Identify and Define the Design Patterns: Document mini-design patterns clearly defining constraints, their applicability, and rationale.
    Define the Role of Standard Elements: Identify and define the roles and usage of standard software elements that can be shared among subsystems, often discovered during common processing identification.

Codeline Models

The codeline model defines the organization and control of the system's code, ensuring order and preventing chaos. It captures essential facts related to:

    Overall structure of the codeline.
    Code control, typically via configuration management.
    Location of different source code types within the structure.
    Maintenance and extension over time, including concurrent development of releases.
    Automated tools for building, testing, releasing, and deploying software.

Specifically, it should define:

    How code is organized into source files.
    How files are grouped into modules.
    The directory structure for files.
    How source is automatically built and tested to form candidate releasable binaries.
    Type and scope of tests to be run regularly and their schedule.
    How binaries are released into test or production environments, ideally automated.
    How source is controlled using configuration management (branching, change sets, etc.) for concurrent development.
    Automated tools for build, test, and release, and their integration for continuous integration and delivery.

The codeline model is crucial for reliable and repeatable build and release processes, especially in distributed development environments.

Notation: While structured notations like UML can be used, a simpler approach with text, tables, and clear diagrams is often sufficient.
Activities for Codeline Models

    Design the Source Code Structure: Design a flexible and easy-to-maintain directory hierarchy for source code that is also simple enough for developers to navigate.
    Define the Build, Integration, and Test Approach: Mandate a common, carefully designed approach for automating build, integration, and testing. This should allow for easy automatic builds and enable developers to use central or local copies of the latest build.
    Define the Release Process: Design a clear, preferably automated, process for releasing work products (binaries, libraries, documentation) after a clean build for testing and use. This includes defining build validation (e.g., automated test suite execution) before release and the use of deployment tools.
    Define the Configuration Management: Establish a common approach to configuration management, encompassing tools, structures (variants, branches, labels), and processes for managing deliverables under control.

Problems and Pitfalls

    Too Much Detail: Avoid defining low-level implementation details in the Development view; these are the concern of designers and implementers.

Risk Reduction

    Minimize the number of design constraints identified.

Development View

This section addresses common problems and risks associated with defining the Development View in software architecture documentation, along with strategies for risk reduction and a checklist of key considerations.
Over-Detailed Architectural Description

Problem: Including excessive detail in the Development View, especially in the common design model, can be counterproductive, leading developers to ignore constraints or struggle to integrate their work. This is particularly problematic for complex systems where the common design model can be extensive and may seem out of place in the main Architectural Description (AD) document.

Risk Reduction:

    Capture detailed system-wide design constraints in a separate document specifically for software developers.
    Summarize the required constraints and their rationale in a short section of the main AD. This allows stakeholders to verify consideration of design constraints without delving into excessive detail.

Uneven Focus

Problem: Architects tend to focus on areas they understand or find interesting, potentially leading to highly detailed descriptions of certain aspects (e.g., network request handling design patterns) while neglecting others (e.g., element initialization processing).

Risk Reduction:

    Adopt a comprehensive perspective, considering all aspects of software development that require architectural definition.
    Seek specialist expertise to advise on unfamiliar areas.

Lack of Developer Focus

Problem: The Development View's primary users are software developers and testers. If the view does not address their questions and concerns, it is likely to be ignored.

Risk Reduction:

    Involve developers and testers in defining the Development View.
    Delegate aspects of the view's development to senior software developers to foster ownership within the development team.

Lack of Precision

Problem: Imprecision in the Development View can lead to misinterpretations or developers ignoring descriptions they cannot understand, especially when the architect lacks expertise in certain areas.

Risk Reduction:

    Review the Development View's contents early with software developers and testers to ensure definitions are sufficiently precise.
    Utilize the knowledge of subject matter experts when experience is limited.

Problems with the Specified Environment

Problem: Specifying aspects of the Development View based on outdated or incorrect knowledge of technologies can lead to development or operational problems and undermine credibility. Additionally, imposing previously successful approaches that are unsuitable for the current project environment (e.g., different needs for short-lived stand-alone systems vs. long-lived product lines) can be detrimental.

Risk Reduction:

    Specify technologies and techniques with which you are genuinely familiar, or obtain trusted, expert advice from subject matter experts.
    Understand the specific needs and constraints of the current project environment, ensuring the Development View accurately reflects them without over-complicating or over-simplifying.
    Delegate research and design of certain Development View aspects to software development team members to alleviate this problem and foster ownership.

Checklist for the Development View

    Has a clear strategy been defined for organizing the source code modules in the system?
    Have general rules been defined governing dependencies between code modules at different abstraction levels?
    Have all aspects of element implementation that require standardization across the system been identified?
    Has the method for performing any standard processing been clearly defined?
    Have any standard design approaches that all element designers and implementers must follow been identified? If so, do software developers accept and understand these approaches?
    Will a clear set of standard third-party software elements be used across all element implementations? Has their usage been defined?
    Will the defined development and test environments work reliably, and be usable and efficient for developers and testers?
    Has a suitable set of tools been defined (by you or others) to reliably automate the end-to-end build, integration, test, and release processes? Does this set include any internal or third-party tools required for deployment to internal or external test and production environments?
    Is this view as minimal as possible?
    Is the presentation of this view in the Architectural Description (AD) appropriate?

Further Reading

    Design Patterns:
        Gamma et al. [GAMM95]
        Coplien et al. [PLOP05–99, PLOP06]
    Configuration Management, Continuous Integration, Automated Testing, Release Processes:
        [AIEL10] - High-level overview, focusing on configuration management and release control.
        [BERC03] - Thorough guide to software configuration management, using patterns.
        [DUVA07] - Thorough and practical guide to continuous integration.
        [HUMB10] - Detailed guide to automating software building, testing, and releasing processes.
        [FREE09] - Practical advice on automated testing, emphasizing its central role in development.