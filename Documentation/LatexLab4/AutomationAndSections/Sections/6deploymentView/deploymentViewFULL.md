The Deployment Viewpoint
Definition & Concerns

    Definition: Describes the environment into which the system will be deployed and its dependencies on elements of that environment.
    Concerns:
        Runtime platform required
        Specification and quantity of hardware or hosting required
        Third-party software requirements
        Technology compatibility
        Network requirements
        Network capacity required
        Physical constraints

Models

    Runtime platform models
    Network models
    Technology dependency models
    Intermodel relationships

Problems and Pitfalls

    Unclear or inaccurate dependencies
    Unproven technology
    Unsuitable or missing service-level agreements (SLAs)
    Lack of specialist technical knowledge
    Late consideration of the deployment environment
    Ignoring intersite complexities
    Inappropriate headroom provision
    Not specifying a disaster recovery environment

Stakeholders

    System administrators
    Developers
    Testers
    Communicators
    Assessors

Applicability

    Systems with complex or unfamiliar deployment environments.
    Useful for any information system with a required deployment environment not immediately obvious to all interested stakeholders.
    Includes:
        Systems with complex runtime dependencies (e.g., specific third-party software packages or particular network services).
        Systems with complex runtime environments (e.g., elements distributed over a large number of machines).
        Systems hosted in third-party environments (e.g., hosting services, public clouds) to clearly define the required environment and deployment method.
        Situations where the system may be deployed into multiple environments and essential characteristics need clear illustration (typical for packaged software products).
        Systems requiring specialist or unfamiliar hardware or software.

Detailed Concerns
Runtime Platform Required

    Identifies the type of runtime platform the system needs and the role of each part.
    Includes:
        General-purpose compute nodes (for servers, computational logic).
        Special-purpose compute nodes (for database engines).
        Storage (for databases, file systems).
        Devices for user access or printing.
        Network services for quality properties (e.g., firewalls for security).
        Specialist hardware (e.g., cryptographic accelerators).
    Manner of platform provision (physical hardware, virtual servers, public cloud) and location of each part must be clearly defined.
    Involves identifying general types of processing elements (e.g., compute server node, application server node, storage array), defining dependencies between them, and mapping functional elements to these types. This forms a logical model of the required runtime platform.

Specification and Quantity of Hardware or Hosting Required

    Details the specific hardware to be procured and commissioned (a physical model of the hardware).
    Hardware may be in-house, third-party, or virtual (e.g., cloud capacity).
    Distinction from "Runtime Platform Required": This concern is more specific and of interest to different stakeholders (e.g., system administrators focus on detailed specs and quantity, developers on general resources).
    Service-level agreements (SLAs) for each part of the runtime environment need to be agreed upon and validated.
    Specific models of equipment or specifications of hosted environment services should be clearly identified and recorded if required; otherwise, precision is still necessary.

Third-Party Software Requirements

    All information systems use third-party software (e.g., operating systems, programming libraries, messaging systems, application servers, databases, Web servers).
    Platform-as-a-service environments may require specific platform services and options.
    The Deployment view must clearly show all dependencies between the system and third-party software products.
    Ensures developers know available software and system administrators know what to install and manage. Helps identify analysis gaps early.

Technology Compatibility

    Each software and hardware element may impose requirements on other technology elements (e.g., database interface library requiring specific OS network library, disk array requiring specific interface type).
    Danger of incompatible requirements when using multiple third-party technologies together (e.g., database library requiring a different OS version than a graphics library supports). Such incompatibilities often emerge late in testing.

Network Requirements

    Derived from inter-element interactions identified in Functional and Concurrency views when elements are hosted on different machines.
    Identifies required links between machines, required capacity, latency, and reliability of links.
    Specifies communications protocols used and any special network functions (e.g., load balancing, firewalls, encryption).

Network Capacity Required

    Often provided by network specialists for the entire organization.
    Software architects need to estimate and record the amount and type of network traffic over each intermachine link in the proposed network topology.

Physical Constraints

    System-level view makes physical constraints important.
    Considerations:
        Desk space for client workstations.
        Floor space for servers.
        Power.
        Temperature control.
        Cabling distances.
    Failure to consider these can prevent system deployment.

Stakeholder Concerns Table (Table 21–1)

    Assessors: Types of hardware or hosting required, technology compatibility, network requirements.
    Communicators: Types and specification of hardware or hosting required, third-party software requirements, network requirements (particularly topology).
    Developers: Types and (general) specification of hardware or hosting required, third-party software requirements, technology compatibility, network requirements (particularly topology).
    System administrators: Types, specification, and quantity of hardware or hosting required; third-party software requirements; technology compatibility; network requirements; network capacity required; and physical constraints.
    Testers: Types, specification, and quantity of hardware or hosting required; third-party software requirements; and network requirements.

Models
Runtime Platform Models

    The core of this view.
    Defines:
        Set of required hardware nodes.
        Which nodes need to be connected to which other nodes via network (or other) interfaces.
        Which software elements are hosted on which hardware nodes.

Deployment View: Runtime Platform Model

This section details the runtime platform model, its elements, notation, and associated activities.
Main Elements of a Runtime Platform Model:

    Processing nodes:
        Represents each computer in the system.
        Allows stakeholders to understand required processing resources.
        Diagram Hint: Summary notation (e.g., UML’s shadow notation) can be used for many similar machines (e.g., Web server farms), but the number of nodes must remain clear.

    Client nodes:
        Represents client hardware, typically with less detail than processing hardware.
        Focus on types and quantities of client machines required.
        Special needs for presentation or user interaction hardware (e.g., touch screens, printers) should be specified.

    Runtime containers:
        Provided by client and server nodes.
        Examples: Software application server, client virtual machine.
        Purpose: Provide a suitable runtime environment for deployed functional elements.

    Online storage hardware:
        Defines:
            Amount and type of storage.
            How it is partitioned.
            Its usage.
            Assumptions about reliability and speed.
            Whether processing occurs close to stored data.
        Types: Disk devices within a processing node or dedicated storage nodes (e.g., disk arrays).
        Diagram Hint: Clear distinction between storage types to show physical impact of separate storage nodes.
        Capacity and possibly speed of each type must be included.

    Offline storage hardware:
        Required for systems dealing with large amounts of information (archives) and for backup of online data.
        Requirements:
            Sufficient capacity.
            Hardware fast enough for acceptable archive/retrieval times.
            Sufficient network bandwidth between online and offline storage.
        Needs to define type, capacity, speed, and location.

    Network links:
        Captures essential connections required by the system.
        Sufficient to show links between hardware nodes at this point.
        More details (e.g., internode bandwidth) captured in the network model.

    Other hardware components:
        Specialized hardware may be considered for:
            Network security.
            User authentication.
            Special interfacing to other systems.
            Specialist processing (e.g., for automated teller machines).

    Runtime element-to-node mapping:
        Mapping of system's functional elements to the processing nodes where they execute.
        Mapping approach depends on concurrency structure:
            If Concurrency view exists: Map operating system processes from that view to processing nodes.
            If no Concurrency view: Map functional elements from the Functional view directly to processing nodes (implying OS process details are not architecturally significant).
        Diagram Hint: Typically captured as a network node diagram showing nodes, storage, interconnections, and software element allocation.

Notation for Runtime Platform Model:

    UML deployment diagram:
        Can document a runtime platform model.
        Shows: Computing "nodes," optional "execution environments" (e.g., runtime containers), "artifacts" (software elements deployed), and "communication paths" between nodes.
        Inter-element dependencies indicated using regular or stereotyped UML dependencies.
        Diagram Hint: "Artifact" can represent deployed binary files (e.g., "OpsPlanner.jar") or entire system elements from the Functional view (e.g., "Data Capture Service"). The «deploy» dependency can record relationships between system elements and deployed artifacts.
        Clarification Needed: UML's general semantics for nodes and communication paths necessitates the use of stereotypes, tagged values, and comments to distinguish node/link types. This diagram type requires augmentation with plain-text descriptions defining the role and characteristics of major elements.

    Boxes-and-lines diagram:
        Simple notation often chosen due to the basic nature of UML deployment diagrams.
        Uses boxes for nodes and elements, arrows for interconnection.
        Annotated as required to clarify meaning.
        Diagram Hint: Requires careful definition of diagrammatic elements to avoid confusion. Easier to draw with non-UML drawing tools and potentially more comprehensible to non-technical stakeholders.

    Text and tables:
        Best for reference information like required hardware specifications.
        Organized into tables for easy, unambiguous reference.

Activities related to Runtime Platform Model:

    Design the Deployment Environment:
        Start by identifying key servers, important client hardware requirements, and necessary network links.
        Elaborate by adding special-purpose hardware (e.g., cryptographic accelerators, redundant capacity nodes) and specifying hardware/software configurations for each node and interconnections.

    Map the Elements to the Hardware:
        Assign each functional (software) element to a home in the proposed deployment environment.
        Iterative process: Mapping may suggest changes in environment design, or new environment options may suggest new software element locations.
        Challenges: Managing dependencies, ensuring sufficient machine capacity, and trading off separated vs. collocated elements (e.g., security vs. performance).

    Estimate the Hardware Requirements:
        Initial estimation before initial deployment environment design, followed by iterative refinement.
        Resources to estimate: Processing power, memory, disk space, and I/O bandwidth for each processing node.

    Conduct a Technical Evaluation:
        May involve prototype element development, benchmarks, and compatibility tests (e.g., checking application server, object persistence library, and database compatibility, and transaction throughput).
        Incomplete Section: Emphasizes identifying key application attributes (size, processing type) for representative tests, involving experts, and arguing for evaluation resources based on risk management. However, specific methods or tools for evaluation are not detailed.

    Assess the Constraints:
        Review proposed deployment environment design against external constraints (formal standards, informal guidelines, implicit constraints).

Deployment View: Network Models

This section describes the network model, its primary elements, notation, and associated activities, typically used when the underlying network is complex.
Introduction to Network Models:

    Usually designed and implemented by networking specialists, not the software architect.
    Architect provides a clear specification of expected network capabilities.
    Description should indicate:
        Nodes needing connection.
        Specific network services required (e.g., firewalls, compression).
        Bandwidth requirements.
        Quality properties required from each part of the network.
    This model is typically a logical or service-based view, not a physical view of individual elements.
    Valuable specification for customers planning software deployment.
    Diagram Hint: Typically represented as an annotated network diagram (a network-oriented specialization of the runtime environment diagram). If network requirements are simple, elaboration of the runtime platform model might suffice.

Primary Elements of a Network Model:

    Processing nodes:
        Represents system elements that use the network to transport data.
        Should match the set from the runtime platform model.
        Abstracted to simple elements with network interfaces.

    Network nodes:
        Additional nodes representing expected network services (e.g., firewall security, load balancing, encryption).

    Network connections:
        Links between network and processing nodes.
        Elaborated to include characteristics of the expected service (most typically bandwidth and latency, but potentially quality of service, reliability, or other network qualities).

Notation for Network Model:

    UML deployment diagram:
        Useful base notation for a network model.
        Clarification Needed: As with the runtime platform description, annotations with stereotypes, tagged values, and comments are likely needed to clarify intentions due to general UML semantics.

    Boxes-and-lines diagram:
        Often used for network models due to similar reasons as for the runtime platform model (simplicity, comprehensibility).

Activities related to Network Model:

    Design the Network:
        Typically handled separately from computer hardware design due to involvement of different specialists.

Deployment View

This section focuses on the detailed design and considerations for deploying the software system within its operational environment, including network design, technology dependencies, and inter-model relationships.
Logical Network Design

    Objective: Sketch network requirements (connections, capacity, quality of service, security).
    Outcome: A logical, rather than physical, network design that serves as a specification for network specialists.
    Capacity and Latency Estimation:
        Estimate capacity and latency between nodes.
        Precision is less critical than realistic magnitude estimation.
        Capacity: Combine peak transaction throughput with approximate message sizes for transaction information.
        Latency: Use standard network metrics (network type, distance) and existing network measurements.
        Apply scaling factors for overheads and prediction inaccuracies.

Technology Dependency Models

    Purpose: Manage software and hardware dependencies in the deployment environment when bundling is not feasible (due to efficiency, cost, licensing, or flexibility).
    Capture Method: Typically captured on a node-by-node basis, often in simple tabular form.
    Software Dependencies: Derived from the Development view, which defines the developer environment.
    Hardware Dependencies: Can be derived from test/development environments, but often rely on manufacturer specifications and testing.
    Example (Primary Server Node Software Dependencies):
        Component: Data Access Service
            Requires: HP-UX 64-bit 11.23 + patch bundle B.11.23.0703, HP aCC C++ runtime A.03.73
        Component: Data Capture Service
            Requires: HP-UX 64-bit 11.23 + patch bundle B.11.23.0703, HP aCC C++ runtime A.03.73, Oracle OCI libraries 11.1.0.7
        Component: HP aCC C++ Compiler & Runtime
            Requires: HP patch PHSS_35102, HP patch PHSS_35103
        Component: Oracle OCI 11.1.0.7
            Requires: HP-UX optional package X11MotifDevKit.MOTIF21, HP-UX patch PHSS_37958
    Detail Level: For complex systems, the Development view contents are unlikely to provide sufficient detail for full software dependency definition per node type.
    Notation:
        Simple Text-Based Approach: Most common and often preferred.
        Graphical Notations: Can be useful but may clutter the runtime platform model if comprehensive.
            Method: Extend runtime platform model to indicate software stack required on each machine.
            Caution: Complete and accurate software dependency stacks can make the runtime platform model unusable; in such cases, record information separately.
        Text and Tables: Almost always used for capturing dependencies. Emphasize exact requirements (detailed version numbers, option names, patch levels).

Activities

    Analyze the Runtime Dependency:
        Manual process of identifying dependencies for system elements and third-party elements.
        Derive dependencies from third-party documentation and internal build/test environment requirements.
        Clearly define third-party elements needed for each processing node.
    Conduct a Technical Evaluation:
        May require prototyping or technical investigation to correctly document dependencies.

Intermodel Relationships

    For complex systems, the Deployment view contains multiple closely related models.
    Models:
        Runtime Platform Model: Core of the view. Referred to by deployment groups early in the project.
        Network Model: Lower layer supporting the runtime platform. Defines network details. Consulted by specialist networking groups.
        Technology Dependency Model: More detailed layer on top of the runtime platform. Defines software and hardware installation requirements for each machine. Used by system administrators for detailed installation planning.
    Relationship Illustration (Figure 21-3 hints):
        Runtime Platform Model: Core.
        Network Model: Provides details of the underlying network.
        Technology Dependency Model: Provides details about hardware and software installed on each node.
    Tooling (Incomplete/Vague):
        Ideal: A software architecture tool would create a single model and extract different aspects automatically.
        Reality: Currently, such tools are not widely available, necessitating work with separate models.

Problems and Pitfalls

    Unclear or Inaccurate Dependencies:
        Issue: Complex computing technology has many explicit and implicit runtime dependencies that, if unsatisfied, cause problems. Many are invisible and difficult to check.
        Example: Discovering wrong utility library version only when database server fails to start.
        Vagueness: Statements like "You need Oracle and Linux" or "It uses Intel hardware" are too vague for safe deployment.
        Requirement: Specify exact versions, optional parts, patch requirements, etc., for enterprise software.
        Risk Reduction:
            Capture clear, accurate, detailed dependencies between software elements and the runtime environment in the Deployment view.
            Capture dependencies between third-party software and its required runtime environment.
            Perform compatibility testing to ensure correct element dependencies.
            Use existing, proven technology combinations with well-understood dependencies.
    Unproven Technology:
        Issue: New technology brings risks (functional shortcomings, inadequate performance, availability, security) due to unknown characteristics.
        Risk Reduction:
            Use existing software and hardware that can be tested before commitment.
            When using new technology: seek advice from experienced users or test thoroughly.
            Create realistic prototypes and benchmarks to verify advertised functionalities.
            Perform compatibility testing with existing technologies.
    Unsuitable or Missing Service-Level Agreements (SLAs):
        Issue: Runtime environment often provided by third parties; SLAs define expected service (cost, performance, reliability, recovery, backup).
        Problem: SLAs may not guarantee system goals.
        Risk Reduction:
            Obtain reliable SLAs for third-party runtime environment elements; estimate internal SLAs if providing elements yourself.
            Attempt to test SLA guarantees.
            Analyze SLAs to understand combined implications.
    Lack of Specialist Technical Knowledge:
        Issue: Designing large systems requires vast specialist knowledge; no single person can be an expert in all technologies.
        Problem: Project teams may lack expertise in all required technologies, leading to reliance on vendor claims instead of proven knowledge.
        Risk Reduction:
            Bring specialist knowledge into the team to master key technologies (full-time or part-time experts).
            Obtain external expert review of the architecture for validation.
            Obtain binding contractual commitments from technology suppliers.
    Late Consideration of the Deployment Environment:
        Issue: Designing purely from a software perspective and considering deployment only after software completion.
        Problem: Inappropriate deployment environment can render a good system unusable.
        Impact: Deployment environment often affects software design and implementation, making late changes expensive (e.g., shifting from a single large machine to a group of small machines for server elements impacts server software architecture).

Deployment View

This section focuses on the considerations and potential pitfalls in designing a system's deployment environment.
General Deployment Design Principles & Risks

    Early Design: Design the deployment environment as part of architecture definition, not as a separate, post-development exercise. Late changes can be expensive.
        Risk Reduction: Integrate deployment environment design early to reduce risks.
    External Expert Review: Obtain external expert review of your architecture for early feedback to avoid costly mistakes.
        Risk Reduction: Seek external review early in the process.

Multisite Deployment Considerations

    Increasing Prevalence: Many systems are deployed across multiple physical sites, increasingly using third-party hosting and cloud environments.
    Impact on Quality Properties: Multisite deployment significantly impacts system quality properties, especially security, performance, and scalability.
    Key Concerns:
        Network Latency: Obvious problem between sites; inter-element interactions across links require careful consideration.
        Security: Maintaining security across multiple sites.
        Scalability Limitations: Potential limitations due to synchronization requirements across sites.
    Risk Reduction:
        Understand multisite deployment requirements as early as possible in design.
        Consider its impact on all system qualities if multisite deployment is likely.
        Collaborate with infrastructure teams to understand implications and restrictions.
        Test representative aspects of multisite deployment early to understand implications.

Inappropriate Headroom Provision

    Definition: Headroom is additional capacity (CPU, memory, disk, network bandwidth, etc.) included in hardware specifications.
    Purpose: Accommodates spikes in demand or future growth, allowing the system to cope with additional demand without immediate hardware upgrades.
    Balance: Requires a delicate balance between optimism for future growth and spending restraint.
    Consequences of Error:
        Too Much: Deploying expensive, underutilized hardware.
        Too Little: System fails to meet performance requirements.
    Risk Reduction:
        Ensure hardware specifications include appropriate headroom.
        Refer to the Performance and Scalability perspective (Chapter 26) for modeling guidance.

Not Specifying a Disaster Recovery Environment

    Definition: Means to keep systems operational in case of significant failures (e.g., power loss, widespread storage failure, natural disaster).
    Strategy: Often requires a separate operational environment at a different location (e.g., standby data center).
    Cost Consideration: Standby environments may have lower specifications to reduce costs.
    Responsibility: Development projects are usually responsible for specifying, implementing, and funding standby hardware.
    Architectural Inclusion: Must be part of the architectural description.
    Further Discussion: Chapter 27 provides additional details.
    Risk Reduction:
        Ensure the Deployment view specifies any required disaster recovery hardware.

Deployment View Checklist

    Functional Element Mapping:
        Have all system functional elements been mapped to a type of element in the runtime platform?
        Have they been mapped to specific hardware devices if appropriate?
    Runtime Platform Understanding:
        Is the role of each piece of the runtime platform fully understood?
        Is the specified hardware or service suitable for the role?
    Hardware/Service Specifications:
        Have detailed specifications been established for hardware devices or hosted services?
        Is the exact quantity of each device or amount of each service known?
    Third-Party Service Level Agreements (SLAs):
        Are SLAs in place for third-party supplied runtime environment elements?
        Are the guarantees suitable for the system?
        Can the credibility of guarantees be tested?
    Third-Party Software Dependencies:
        Have all required third-party software been identified?
        Have all dependencies between system elements and third-party software been documented?
    Network Topology & Services:
        Are the required network topology and services understood and documented?
        Have network capacity been estimated and validated?
        Can the proposed network topology support the required capacity?
        Have network specialists validated the network buildability?
    Compatibility Testing:
        Has compatibility testing been performed when evaluating architectural options to ensure elements of the proposed deployment environment can be combined as desired?
    Validation through Testing:
        Have prototypes, benchmarks, and other practical tests been used to validate critical aspects of the proposed deployment environment?
    Test Environment:
        Can a realistic test environment representative of the proposed deployment environment be created?
    Deployment Environment Confidence:
        Is there confidence that the deployment environment will work as designed?
        Has external review been obtained to validate this opinion?
        Are assessors satisfied that the deployment environment meets their requirements (standards, risks, costs)?
    Physical Constraints:
        Have physical constraints (floor space, power, cooling, etc.) implied by the required deployment environment been checked for feasibility?
    Headroom:
        Do hardware and service specifications include appropriate headroom?
    Disaster Recovery:
        Does the Deployment view include a specification of a disaster recovery environment, if required?