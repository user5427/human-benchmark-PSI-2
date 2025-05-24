Achieving Consistency Across Views

Purpose of Views:

    To represent a large and complex system in a way stakeholders can understand.
    A view portrays aspects or elements of the architecture relevant to specific concerns and stakeholders.

Problems Without Views:

    A single, all-encompassing model becomes complex and uses a mix of notations, making it difficult to understand the system and architectural choices.

Challenges with Partitioning (Using Views):

    Difficulty in ensuring consistency between views.
    Structures, features, and elements in one view must be compatible and aligned with other views.
    Consistency is vital for Architectural Description (AD) to ensure the system works, meets design goals, and is buildable.

Tooling Limitations:

    No currently available tools automate consistency checking to the required extent.
    Formal modeling languages (e.g., UML) and their tools provide only basic consistency checks between models.
    Informal or custom notations worsen the problem.

Strategies for Achieving Inter-View Consistency:

    Focus on consistency from the outset:
        Do not wait until models are nearly complete to check for consistency; this will likely lead to significant rework.
    Enumerate model elements:
        Assign unique identifiers to significant model elements to simplify consistency checks (e.g., "Is element 3 from Model B consistent with element 5 from Model D?").
    Ensure that consistency checks are a formal part of reviews:
        Consistency (both internal and external) should be a review criterion for models and other architectural documentation.
        Results and actions of formal consistency checks should be included as an appendix in the AD.

Relationships Between Views

    All views are interrelated, but strong dependencies exist only between some.
    Strong dependency (indicated by an arrow) implies a change at the arrow's end will likely require a change at its start.
    No dependency between views means a change in one is unlikely to necessitate a change in the other (e.g., changing a Development view element does not inherently imply changes to Functional models, unless the change reason is unrelated to development).
    If a specific view (e.g., Concurrency view) is not developed separately (e.g., aspects encapsulated in the Functional view), applying relevant consistency checklists is still useful to ensure concerns are addressed.

Consistency Checks Between Specific Views
Context and Functional View Consistency

    Goal: To ensure system scope and requirements are fully and correctly implemented by the system.
    Checklist:
        Does each requirement map to one or more implementing functional elements?
        Is every functional element necessary (directly or indirectly) to implement at least one requirement?
        Have all quality properties affecting system functionality been considered in the Functional view's system structure?
        Are all external entities defined in one view also present and consistently defined in the other?
        Are all interfaces defined in one view also present and consistently defined (responsibilities, nature, characteristics) in the other?
        Are interaction scenarios defined in the Context view compatible with the functional structure and inter-element/external interactions?

Context and Information View Consistency

    Goal: To ensure data flows in and out of the system are compatible with the information management approach defined in the Information view.
    Checklist:
        Has the Information view considered all data items flowing into the system from the Context view (ownership, consistency, timeliness, etc.)?
        Has the Information view considered all data items flowing out of the system from the Context view (ownership, consistency, timeliness, etc.)?
        Have all quality properties affecting information management been considered in the Information view?
        Is the data ownership model in the Information view (especially for externally owned data) compatible with responsibilities defined for external entities in the Context view?
        Is the high-level data model in the Information view compatible with data models used by external systems, or are appropriate data transformation mechanisms defined?
        If external archiving services are defined in the Information view, are they represented as external entities in the Context view?

Context and Deployment View Consistency

    Goal: To ensure external connections between this system and others can be supported in the planned deployment environment.
    Checklist:
        Do all external entities representing systems, interfaces, or technology-based connections appear consistently in both the Context and Deployment views?
        Does the Deployment view contain all hardware and software required to communicate with external entities identified in the Context view?
        Is the technology used for each interface in the Deployment view appropriate for its nature and characteristics as defined in the Context view?
        Are system elements that communicate with external entities deployed to parts of the deployment environment where external communication is possible (e.g., to a DMZ in the network)?
        Have all quality objectives identified in the Context view that affect the deployment environment been taken into account in the Deployment view?

Functional and Information View Consistency

    Goal: To ensure functional and information structures are compatible and that nothing is missing in one that is required by the other.
    Checklist:
        Does every nontrivial functional element in the Functional view requiring persistent data have corresponding data elements in the Information view?
        Does every nontrivial data element in the Information view have at least one element in the Functional view responsible for its maintenance?
        If information flows are described in the Information view, are they consistent with inter-element interactions in the Functional view?
        If the Information view requires specific functional features (e.g., distributed transaction support, redundant logging), are these addressed in the Functional view?
        Do data ownership models in the Information view align with the functional structure in the Functional view?
        If data ownership characteristics are complex (e.g., multiple creators/updaters), do functional models reflect requirements for maintaining distributed data consistency?
        If there are significant issues with maintaining distributed identifiers (keys), do functional models include features to address these?
        If the architecture has significant data migration and data quality analysis aspects, are there corresponding functional elements in the Functional view?
        If loose coupling is an architectural goal for the functional structure, is this reflected (as far as possible) in the static information structure?

Functional and Concurrency View Consistency

    Goal: To ensure functional elements are mapped to tasks for execution and inter-element interactions are supported by interprocess communication if required.
    Checklist:
        Is every functional element in the Functional view mapped to a concurrency element (process or thread) responsible for its execution in the Concurrency view?
        If functional elements are partitioned into separate processes, are suitable interprocess communication mechanisms used to allow all inter-element interactions shown in the Functional view?
        If multiple functional elements are packaged into a single process, is it clear which element controls the process?

Functional and Development View Consistency

    Goal: To ensure all functional elements are mapped to design-time modules, and common processing, test approach, and codeline are compatible with the proposed functional structure.
    Checklist:
        Does the code module structure include all functional elements that need to be developed?
        Does the Development view specify a development environment for each technology used by the Functional view?
        If the Functional view specifies a particular architectural style, does the Development view include sufficient guidelines and constraints to ensure correct implementation of the style?
        Where common processing is specified, can it be implemented straightforwardly over all elements defined in the Functional view?
        Where reusable functional elements are identified from the Functional view, are these modeled as libraries or similar features in the Development view?
        If a test environment has been specified, does it meet the functional needs and priorities of the elements defined in the Functional view?
        Can the functional structure described in the Functional view be built, tested, and released reliably using the codeline described in the Development view?

Functional and Deployment View Consistency

    Goal: To ensure each functional element is correctly mapped to its deployment environment.
    Checklist:
        Has each functional element been mapped to a processing node to allow it to be executed?
        Where functional elements are hosted on different nodes, do the network models allow the required element interactions to occur?
        Are functional elements hosted as close as possible to the information they need to process?
        Are functional elements that need to interact extensively hosted as close as possible? (Incomplete section: The sentence cuts off here. It is likely implying "as close as possible to each other" or "to their interacting counterparts.")

Inter-View Consistency Checks

This section outlines consistency goals and checks between various architectural views. It focuses on ensuring that decisions made in one view are compatible with and supported by the other views.
Functional and Operational View Consistency

Goal: To ensure that each specified functional element can be installed, used, operated, managed, and supported.

Checks:

    Installation and Upgrade:
        Does the Operational view clearly explain how every functional element will be installed?
        Does the Operational view clearly explain how every functional element will be upgraded, if necessary?
    Migration:
        If migration is required, does the Operational view clearly explain how migration will occur for every functional element that needs it?
    Monitoring and Control:
        Does the Operational view explain how each functional element will be monitored and controlled in the production environment?
    Configuration Management:
        Does the Operational view explain how the configuration of each functional element will be managed in the production environment?
    Performance Monitoring:
        Does the Operational view explain how the performance of each functional element will be monitored in the production environment?
    Support:
        Does the Operational view explain how each functional element will be supported in the production environment?
    Simplicity of Approaches:
        Are the approaches specified in the Operational view for installation, migration, monitoring, control, and support the simplest set that will support the needs of the system’s functional elements?

Information and Concurrency View Consistency

Goal: To ensure that the concurrency structure of the system does not cause data access problems and that the proposed information structure is compatible with the concurrency structure.

Checks:

    Concurrent Data Access Protection:
        Does the concurrency design imply concurrent access to any of the system's data elements?
        If so, have the data elements been protected from concurrent access problems?
    Data Availability for Packaged Elements:
        When functional elements are packaged into operating system processes, is the data they require still available to them?
    Interprocess Data-Sharing Mechanisms:
        If functional elements that share data elements are packaged into different operating system processes, has a suitable interprocess data-sharing mechanism been defined?

Information and Development View Consistency

Goal: To ensure that the proposed development environment can provide the technical resources required to develop the data management aspects of the system.

Checks:

    Development Tools and Environment for Data Management Technology:
        Does each data management technology identified in the Information view have defined development tools and environment?
    Sizing Reflection of Data Volumes:
        Does the sizing of the development environments and test data platforms reflect the data volumes created in the Information view?
    Development Support for Migration Data:
        If the Information view defines a significant migration data aspect, are there defined development tools and environments to support this?
    Consideration of External Data Components:
        If the Information view defines external data components (e.g., for existing systems or external systems under construction), does the Development view take this into account (e.g., creation of stub environments, realistic test data)?

Information and Deployment View Consistency

Goal: To ensure that the proposed deployment environment provides the resources required to support the defined information structure.

Checks:

    Sufficient Storage:
        Does the Deployment view include enough storage (of the appropriate types) to support the information storage approach specified by the Information view?
    Fast and Reliable Links for Separate Storage:
        If separate storage hardware is used, does the Deployment view specify sufficiently fast and reliable links from the storage to the processing hardware?
    Backup and Recovery Requirements:
        Does the Deployment view reflect the requirements for backup and recovery as addressed by the Information view?
    Bandwidth for Large Information Volumes:
        If large volumes of information need to be moved, is sufficient bandwidth available so that this can be achieved without critically impacting the operation of the system?

Information and Operational View Consistency

Goal: To ensure that the system's information structure can be installed, used, operated, managed, and supported.

Checks:

    Installation Steps for Data Management Technology:
        Does the Operational view clearly indicate whether specific installation steps are required for the system's data management technology?
    Data Migration:
        If migration is required, does the Operational view clearly explain how data migration will occur?
    Monitoring and Control of Data Management Technology:
        Does the Operational view explain how the data management technology will be monitored and controlled in the production environment?
    Configuration Management of Data Management Technology:
        Does the Operational view explain how the configuration of the data management technology will be managed in the production environment?
    Performance Monitoring of Data Management Technology:
        Does the Operational view explain how the performance of the data management technology will be monitored in the production environment?
    Support for Data Management Technology:
        Does the Operational view explain how the data management technology will be supported in the production environment?

Concurrency and Development View Consistency

Goal: To ensure that the concurrency structure specified in the Concurrency view can be built and tested in the development environment specified by the Development view.

Checks:

    Design Patterns for Complex Concurrency:
        If the concurrency structure is complex, are sufficient design patterns specified in the Development view to guide its implementation?
    Codeline Support for Packaging:
        Does the codeline defined in the Development view support the packaging of the system's functional elements into the operating system processes specified by the Concurrency view?
    Test Approach Support for Concurrency:
        Does the test approach defined in the Development view support testing of the concurrency structure specified in the Concurrency view?
    Development Environment for Concurrency:
        Does the development environment defined in the Development view allow development and testing of the concurrency structure specified in the Concurrency view?

Concurrency and Deployment View Consistency

Goal: To ensure that the system's runtime tasks are correctly mapped to execution resources.

Checks:

    Process to Processing Node Mapping:
        Is every operating system process mapped to a processing node to allow it to run?
    Interprocess Communication Facility Implementation:
        Can the interprocess communication facilities used in the Concurrency view be implemented on and between the processing nodes specified in the Deployment view?
    Processing Node Power for Processes:
        Are the processing nodes specified in the Deployment view sufficiently powerful to host the processes mapped to them from the Concurrency view?
    Full Utilization of Processing Nodes:
        Is every processing node in the Deployment view fully used by the processes mapped to it?

Deployment and Operational View Consistency

Goal: To ensure that the deployment environment described in the Deployment view can be installed, used, monitored, managed, and supported.

Checks:

    Installation of Deployment Environment Elements:
        Does the Operational view define how each of the elements in the deployment environment will be installed?
    Monitoring and Control of Deployment Environment Elements:
        Does the Operational view describe how each of the elements in the deployment environment can be monitored and controlled?
    Existing, Buy, or Develop Monitoring/Control Facilities:
        Does the Operational view clarify which monitoring and control facilities already exist, which can be bought, and which must be developed?
    Supportability of Deployment Environment Elements:
        Can each of the elements in the deployment environment be supported in the organization?