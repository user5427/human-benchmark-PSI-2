The Functional Viewpoint

This section details the Functional Viewpoint of the system architecture, focusing on runtime functional elements, their responsibilities, interfaces, and interactions.
Definition and Core Purpose

    Definition: Describes the system’s runtime functional elements and their responsibilities, interfaces, and primary interactions.
    Demonstrates: How the system will perform its required functions.
    Cornerstone: Often the first and sometimes the only view stakeholders read.
    Drives: Definition of other architectural views (particularly Information, Concurrency, Development, and Deployment).
    Challenge: Including an appropriate level of detail, focusing on architecturally significant elements.
    Avoid: Documenting physical implementation details (e.g., servers, infrastructure), which belong in the Deployment view.

Concerns

This view addresses the following key concerns:

    Functional Capabilities: Defines what the system is required to do (and implicitly, what it is not required to do).
        Emphasis on showing how architectural elements provide agreed-upon functionality.
        Importance of clear definition of system scope, especially when requirements are not fully established at the outset.
    External Interfaces: Describes data, event, and control flows between the system and other external systems.
        Data Flow: Can be inward (system state change) or outward (result of system state change).
        Events: Can be consumed (notifications for system) or emitted (notifications for other systems).
        Control Flow: Can be inbound (external request to system) or outbound (system request to external).
        Interface Definition: Requires consideration of both syntax (structure) and semantics (meaning/effect).
    Internal Structure: Defines the system's internal elements, their mapping to requirements, and their interactions.
        Impacts quality properties like availability, resilience, scalability, and security.
        Choice among various design options (monolithic, loosely coupled, standard packages, network-accessible services) to meet requirements and quality properties.
    Functional Design Philosophy: How well the architecture adheres to established principles of sound design.
        Important for technical stakeholders (development, test teams) for ease of build, test, operation, and enhancement.
        Implicitly desired by acquirers for faster, cheaper, and easier production.
        Underpinned by design characteristics.

Models

    Functional structure model

Problems and Pitfalls

    Poorly defined interfaces
    Poorly understood responsibilities
    Infrastructure modeled as functional elements
    Overloaded view (too much detail or scope)
    Diagrams without element definitions
    Difficulty in reconciling needs of multiple stakeholders
    Wrong level of detail
    "God elements" (elements with excessive responsibilities)
    Too many dependencies

Stakeholders

    All stakeholders

Applicability

    All systems

Design Characteristics (Underpinning Functional Design Philosophy)

The design philosophy is supported by several characteristics, impacting system qualities, particularly evolution, flexibility, and maintainability, as well as performance and security.

    Coherence
        Description: Logical structure, elements working together to form a whole.
        Significance: Indicates correct element decomposition; aids stakeholder understanding.
    Cohesion
        Description: Extent to which an element's functions are strongly related.
        Significance: Grouping related functions results in simpler, less error-prone designs.
    Consistency
        Description: Mechanisms and design decisions applied consistently throughout the architecture.
        Significance: Easier to build, test, operate, and evolve than inconsistent systems.
    Coupling
        Description: Strength of element interrelationships; extent to which changes in one module affect others.
        Significance: Loosely coupled systems are easier to build, support, and enhance, but may be less efficient than monolithic approaches.
    Extensibility
        Description: Ease of extending the architecture to perform new functions in the future.
        Significance: Often a result of coherence, low coupling, simplicity, and consistency.
    Functional Flexibility
        Description: Amenability of the system to supporting changes to already provided functions.
        Significance: Systems designed for easy change are typically harder to build and less efficient.
    Generality
        Description: Mechanisms and decisions in the architecture are as general as practicable.
        Significance: Generic solutions lead to extensibility and change, but must be balanced against cost and complexity.
    Interdependency
        Description: Proportion of processing steps involving interactions between elements vs. within an element.
        Significance: Communication between certain element types can be significantly more expensive and less reliable than internal operations.
    Separation of Concerns
        Description: Extent to which each internal element is responsible for a distinct part of the system's operation; common processing performed in only one place.
        Significance: High separation results in easier to build, support, and enhance systems, but may adversely impact performance and scalability compared to a monolithic approach.
    Simplicity
        Description: Use of the simplest suitable design solutions within the system.
        Significance: Complexity hinders building, comprehension, operation, and evolution; however, a simplistic approach may not meet sophisticated system requirements.

Chapter 17: The Functional Viewpoint
Stakeholder Concerns for the Functional Viewpoint

Table 17–2 lists typical stakeholder concerns for the Functional viewpoint:

    Acquirers: Primarily functional capabilities and external interfaces.
    Assessors: All concerns.
    Communicators: Potentially all concerns, to some extent, depending on context.
    Developers: Primarily design quality and internal structure, but also functional capabilities and external interfaces.
    System administrators: Primarily functional design philosophy, external interfaces, and possibly internal structure.
    Testers: Primarily design quality and internal structure, but also functional capabilities and external interfaces.
    Users: Primarily functional capabilities and external interfaces.

Functional Structure Model

The functional structure model typically contains the following elements:

    Functional elements:
        A well-defined runtime (as opposed to design-time) part of the system.
        Has particular responsibilities.
        Exposes well-defined interfaces for connection to other elements.
        Can be a software code module, application package, data store, or a complete system.
    Interfaces:
        A well-defined mechanism for other elements to access an element's functions.
        Defined by inputs, outputs, semantics of each operation, and interaction nature.
        Common types in information systems: remote procedure calls (RPCs), messaging, events, interrupts.
    Connectors:
        Pieces of the architecture that link elements for interaction.
        Define interaction between elements and allow interaction nature to be considered separately from operation semantics.
        Consideration depends on circumstances:
            Simple procedure call: Acknowledge connection.
            Message-based interface: Can be defined as a type of element, providing capabilities to interactions.
        Focus on architecturally significant aspects.
    External entities:
        Other systems, software programs, hardware devices, or any other entity with which the system interacts.
        Obtained from the system’s Context view.
        Appear in the functional model at the far end of an interface, external to the system.

Exclusions from Functional Structure Model:

    Does not define how code is packaged and executed in processes and on threads (domain of Concurrency and Deployment views).
    Generally, underlying infrastructure should not be modeled as functional elements unless it performs a functionally significant task independent of other functional elements, without which the view is incomplete.
    Infrastructure that simply supports functional elements operation should typically not be shown (best considered in Deployment view).
        Example: Message queues (interelement connectors) might be shown, but the message broker providing them would typically not be shown in this view, appearing instead in the Deployment view.

Notation

Various techniques can represent the Functional view in a model:

    UML Component Diagrams:
        Advantages: Widespread comprehension, flexibility.
        Main Diagram: Component diagram, showing elements, interfaces, and interelement connections.
        Typical Elements (Figure 17-1 Example):
            System with two internal elements (Variable Capture, Alarm Initiator) and one external element (Temperature Monitor).
            Variable Capture exposes VariableReporting interface (invoked by Temperature Monitor).
            Alarm Initiator exposes LimitCondition interface (invoked by Variable Capture).
            VariableReporting tagged with XML RPC, HTTP protocol, max 10 concurrent invocations.
        Representation:
            System elements and external entities: UML component icon, annotated with name and stereotype (e.g., <<external>> for external entities, <<infrastructure>> for distinct functional infrastructure elements).
            Interfaces: UML "lollipop" interface icon (preferred over stereotyped class icon).
            Interface Differentiation: Stereotypes with tagged values (e.g., "transport") for characteristics like type, protocol, concurrent users/connections.
            Connectors: UML dependencies and information flows.
        Example (Figure 17-2):
            System: Web storefront (Web Shop) for online catalog purchases, integrating with existing enterprise software.
            External Entities: Web browsers of three user types (customers, customer care representatives, catalog administrators), external Order Fulfillment System.
            Internal Functional Components: Web Shop, Product Catalog, Order Processor, Customer Information System, Stock Inventory.
            Connector Types: HTML over HTTP, publish/subscribe messaging, LU 6.2 external interface.
            Interactions:
                Customers order from Web Shop, interacting with Product Catalog, Order Processor, Customer Information System.
                Catalog administrators maintain product catalog via Web-based interface.
                Customer care representatives maintain customer information via dedicated client (Customer Care Interface).
                Product Catalog accesses Stock Inventory for stock levels.
            Concurrency Insights: Up to 1,000 customers, 80 customer care representatives, and 15 catalog administrators simultaneously.
            Protocol Insight: Interaction between Product Catalog and Stock Inventory uses a specific protocol.
            Assumed Communication: Unadorned intercomponent communication via standard remote procedure call (definition assumed to be elsewhere).
            Incomplete Section/Vague Statement: The example explicitly states that component responsibilities, interface details, and interaction details are not obvious from the diagram, emphasizing the need for underpinning textual descriptions and multiple models (e.g., system scenario modeling for intercomponent interactions). This highlights an area requiring significant clarification and detail for downstream processing.
    Other Formal Design Notations:
        Includes older structured notations (e.g., Yourdon, Jackson System Development, Object Modeling Technique).
        Limitations:
            Tend to be weak at describing architecturally important concepts (large-scale elements, interfaces, deployment options).
            Less widely taught or used today, potentially lacking tool support and general familiarity.
    Architecture Description Languages (ADLs):
        Languages directly supporting concepts for software architects (e.g., Unicon, Wright, xADL, Darwin, C2, AADL).
        Attraction: Native support for architectural concepts like components and connectors.
        Practical Drawbacks (Incomplete Section/Vague Statement):
            Primarily developed in research environments.
            Lack of stakeholder familiarity.
            Relatively narrow scope (often only components and connectors).
            Inevitable lack of mature tool support.
            The authors state they haven't found a suitable ADL for day-to-day adoption despite years of searching. This implies a lack of readily available, practical ADLs for this purpose.
    Boxes-and-Lines Diagrams:
        Custom notation used by many architects for functional structure diagrams.
        Should show only functional elements and their interfaces.
        Elements linked to interfaces with clear graphical devices (e.g., arrow, with annotation) indicating connector use.
        Requirement: Clearly define the meaning of the custom notation to avoid confusion.

Functional View

This section describes the Functional View of the software system, focusing on its functional elements, their responsibilities, and the interfaces between them.
Diagrammatic Representations and Notations

    Boxes-and-Lines Diagram (Figure 17–3):
        Purpose: Provides an alternative, less formal, and more user-friendly representation for nontechnical stakeholders (business users, sponsors).
        Notation:
            Functional elements: Rectangles.
            Links between elements: Lines with arrows indicating direction(s) of information flow.
            External user-facing interfaces: Icon resembling a computer monitor.
            External back-end systems: Rectangles with rounded corners.
            Data stores: Icon resembling a disk drum.
            Functional interfaces (e.g., Internet, message bus): Cloud icon.
            System scope: Elements within a dotted rectangle.
        Benefits: Easier for nontechnical stakeholders to understand, useful for selling system features and benefits.
        Usage: Can be used as a front for more detailed, rigorous UML models. Requires a defined standard notation to be adhered to. Icons should suggest the underlying purpose of the modeled elements.
        Support: Must be supported by a definition of its elements and their interfaces, presented in a standardized way.

    Sketches:
        Purpose: Create a less formal feel for the view by introducing ad hoc notation as required to represent significant aspects. Useful for communicating essential aspects to nontechnical stakeholders.
        Problem: Can lead to a poorly defined view and confusion.
        Mitigation: Use to augment a more formal view notation (e.g., UML), and use different notations for different stakeholder groups.

    Modeling Message-Oriented Interactions:
        Challenge: More difficult to model than procedure-oriented interactions.
        Older Approach: Show message distribution mechanism (e.g., message-oriented middleware) as a functional element, connecting sources and destinations to it. (Difficulty: discerning overall message flow).
        Preferred Approach (Garland and Anthony [GARL03]): Use ports and information flows.
            Ports: Abstract representations of message sources or destinations. Integrated into UML 2.
            UML Model Example (Figure 17–4):
                Illustrates a notional financial system where a Price Calculator distributes prices via asynchronous messages.
                Small boxes attached to system elements: Ports.
                Price Calculator port: Output port (creates messages).
                Other elements' ports: Input ports (receive messages).
                UML 2 information flow connector: Indicates message flow between elements.
                Stereotype: Indicates messaging type (e.g., publish/subscribe).
                "Information conveyed" annotation: Captures message type (e.g., "Prices").
        Benefits: Clearly shows messaging within a system, allows combination with procedure-oriented interactions on a single diagram without confusion. Can model higher-level messaging systems (e.g., EAI architectures).

    Functional View Scope:
        Should describe only the system’s functional elements.
        Incomplete Section/Vague Statement: The text indicates that if "notational items to represent deployment, concurrency, or other aspects of the system" are present, the Functional view is "overloaded." This implicitly suggests a clear boundary for the Functional view, but doesn't explicitly define what constitutes "functional elements" in contrast to "deployment, concurrency, or other aspects" beyond saying "describe only the system's functional elements." This could benefit from more explicit boundary definitions for clarity to prevent misinterpretation by downstream models.

    SysML (Systems Modeling Language):
        Description: Design language for systems engineering, based on UML 2 (a UML 2 profile).
        Relevance to Information Systems Design: Not found to be a better alternative to UML 2 for information systems design.
        Purpose: Aimed at integrating hardware, software, personnel, facilities, and other varied aspects of very large systems, rather than the more focused problem of information system design.
        Resources: sysml.org, omgsysml.org, sysmlforum.com.

Activities
Identify the Elements

Steps for identifying functional elements:

    Work through functional requirements to derive key system-level responsibilities.
    Identify functional elements that will perform those responsibilities.
    Assess the identified set against desirable design criteria.
    Iterate back to refine the functional structure until it is judged sound.

    Preexisting Elements: For defined elements (e.g., software libraries, packages, existing systems/subsystems), the process is understanding rather than identifying and designing.
    Refinements to Functional Structure:
        Generalization: Identify common responsibilities across elements and introduce more general, reusable elements (important for enterprise/product-line architecture for software asset reuse).
        Decomposition: Break large, complex elements into smaller subelements (for large systems, to create manageable subsystem-level elements).
        Amalgamation: Replace a number of small functional elements with a larger element encompassing their functions (typically used when many small, similar elements are identified, to factor out commonality and reduce interactions).
        Replication: Replicate a system element or a piece of processing (e.g., data validation for incoming data across external interfaces). (Benefits: performance; Caution: maintaining consistency).
    Architectural Style Influence: If using an architectural style, the process involves creating an instantiation of the style and assigning system-level responsibilities to its elements.
    Further Reading: The document explicitly defers detailed discussion of element identification methods, citing numerous approaches dependent on system type and software development approach (procedural, object-oriented, component-based). (Refer to "Further Reading" section of the original chapter).

Assign Responsibilities to the Elements

    Activity: Assign clear responsibilities to identified candidate elements.
    Responsibilities Include: Information managed by the element, services offered to other system parts, and activities initiated.
    Example (Table 17–3):
        Element Class: Web Shop
            Present customers with an HTML-based user interface accessible with a Web browser.
            Manage all state related to the customer interface session.
            Interact with other system parts to allow customers to view catalog and stock levels, buy goods, and view customer information.
        Element Class: Customer Information System
            Manage all persistent information about customers of the system.
            Provide a query-only interface for retrieving customer information visible to that customer.
            Provide an information management programmatic interface for creating customer information management applications.
            Provide an event-driven message-handling interface to accept details of orders placed by customers and their state changes.

Design the Interfaces

    Requirement: Services offered by elements must be accessed via well-defined interfaces.
    Interface Definition Content:
        Operations offered.
        Input, outputs, preconditions, and effects of each operation.
        Nature of the interface (e.g., messaging, remote procedure call, Web service).
    Design Approach Recommendation: Design by Contract
        Creator: Bertrand Meyer (for object-oriented systems).
        Method: Define interfaces via "contracts" using preconditions, postconditions, and invariants to precisely define operation behavior and relationships.
    Notation for Interface Definition: Depends on interface type and target audience. Considerations include likely implementation technology, development team background, and interface kinds.
    Common Interface Definition Notations:
        Programming Languages:
            Method: Define operation signatures directly using a programming language, with text/language assertions for semantics.
            Pros: Simple.
            Cons: Ties definition to style, assumptions, and limitations of the specific language; may not be ideal for multiple technologies.
            Best Use: For programming libraries, or when the system is a single, large programming artifact, or a single programming language is used for the entire system.
        Interface Definition Languages (IDLs):
            Purpose: Developed to support mixed-language distributed systems technology (e.g., CORBA IDL, .NET IDL, WSDL for Web services).
            Characteristics: Independent of implementation technology; offer simpler facilities than programming languages, more suitable for architectural interfaces.
            Benefit: Good option for defining operation signatures if stakeholders can read or be taught to read them.

Functional View

This section details aspects of the Functional View, focusing on interfaces, connectors, and analysis activities to ensure a robust and well-defined system structure.
Interfaces
Data-oriented Approaches

Interfaces can be defined purely in terms of exchanged messages.

    Examples:
        Interfaces accessed via messaging systems.
        Interfaces defined by structured document exchange (e.g., document-oriented, Web-service-based interfaces with XML Schema messages).
    Suitability: Particularly effective for event-based interfaces defined by business event exchange rather than operation invocation.

Interface Semantics and Definition

    Beyond Simple Definitions: An interface is more than a simple definition of operation calls.
    Defining Semantics:
        Current approaches lack facilities for defining interface semantics.
        Requires natural language or specialist languages (e.g., Object Constraint Language (OCL)).
    Required Content: A clear interface definition must accurately communicate:
        Pre- and postconditions of each operation.
        How operations combine to perform useful functions.
        Examples are preferred.
    Consequence of Omission: Lack of clear semantic definitions will likely cause significant problems during interface usage.

Connectors
Design and Purpose

    Necessity: System elements require communication to achieve goals; interactions arise from element responsibilities.
    Mechanism: Interactions occur across connectors linking delegating elements to interfaces offered by other elements.
    Connector Types:
        Sometimes self-evident (e.g., simple procedure call).
        Requires careful consideration for synchronous vs. asynchronous communication, resiliency, acceptable latency.
    Modeling: For each required interelement communication path, a connector (e.g., RPC, messaging, file transfer) should be added to the model.

Analysis and Evaluation Activities
Functional Traceability Check

    Purpose: Ensure all functional requirements (from documentation) are met by the proposed functional structure.
    Benefits: Often reveals missing or incomplete functions in the functional structure model.
    Formal Capture (Optional): Usually presented as a table cross-referencing functional requirements against functional model elements responsible for them.

Common Scenarios Walkthrough

    Value: Extremely valuable and illuminating to walk through common system usage scenarios with stakeholders using the Functional view.
    Target Audience: Testers, development team, system administrators.
    Process: Explain how system elements interact to implement the scenario.
    Outcomes: Identifies architectural weaknesses, misunderstandings, and missing elements.
    Context: Can be part of a larger architectural assessment.

Interaction Analysis

    Purpose: Analyze the chosen structure based on the number of interelement interactions during common processing scenarios.
    Impact of Excessive Interactions: High interelement interactions can negatively impact the system.
    Refinement Goal: Reduce interelement interactions to a minimum set without distorting component coherence.
    Desired Result: Well-structured system with cohesive, loosely coupled elements; typically leads to efficient and reliable systems.
    Tradeoffs: When performing interaction analysis, ensure that reducing interactions does not lead to distorted system structure, undesirable redundancy, or inappropriate element partitioning.

Flexibility Analysis

    Rationale: Successful systems are constantly under pressure to change.
    Focus: Assess architectural flexibility early in the project.
    Key Factor: Functional structure often heavily influences information system flexibility.
    Method: Work through "what if" scenarios to reveal the impact of future changes.
    Conflict: Changes suggested by flexibility analysis may conflict with those from interaction analysis.
    Resolution: Trade off these two factors during architectural evaluation to find the right balance; avoid unnecessary design complexity.
    Context: Part of architectural evaluation activities.

Common Problems and Pitfalls
Poorly Defined Interfaces

    Issue: Neglecting connectors and interface definitions despite well-defined elements, responsibilities, and relationships.
    Consequences: Major misunderstandings between subsystem development teams leading to build errors, incorrect behavior, and subtle system unreliability.
    Risk Reduction:
        Define interfaces and interelement connectors clearly and early.
        Review interfaces and connectors frequently for understanding.
        Consider element definition incomplete until interfaces are designed.
        Interface definitions must include operations, their semantics, and examples.

Poorly Understood Responsibilities

    Issue: Focusing only on key scenarios, leading to confusion over functional element responsibilities if not fully defined and traced.
    Consequences: Missing functionality (falling between gaps) or duplicated functionality (multiple teams assuming responsibility).
    Risk Reduction:
        Formally define element responsibilities as early as possible.
        Prevent drift into element design without formal, agreed-upon responsibilities.
        Ensure all implementers understand their boundaries.
        Map all requirements to implementing elements.

Infrastructure Modeled as Functional Elements

    Issue: Including underlying infrastructure as functional elements in the Functional view.
    Consequences: Makes the view more confusing without adding useful information.
    Best Practice: Infrastructure can generally be hidden within functional elements; the Deployment view defines infrastructure details.
    Exception: Include infrastructure elements only if crucial for understanding the Functional view (e.g., a messaging gateway performing functional processing).
    Risk Reduction:
        Avoid modeling underlying infrastructure elements initially; focus on functional elements addressing the problem domain.
        Question elements not named in relation to the problem domain.
        Address specific infrastructure concerns in other views (typically Deployment view).

Overloaded View

    Issue: Allowing the Functional view to become a "compound view" by implicitly adding deployment, concurrency, or other architectural aspects.
    Consequences: Unclear description, difficult to understand, limited use for stakeholders.
    Example (Figure 17-5 - Not Provided): An example diagram with ad hoc notation (e.g., dashed lines from "Socket Library" to "Web Server," within "Server Node(s)") leading to ambiguity regarding its meaning.
        Incomplete Section: The example Figure 17-5 is referenced but not provided in the input, making it difficult to fully understand the specific visual cues and their misinterpretations.
        Vague Statement: "The system provides a salesperson with an interface to allow something (perhaps a holiday or flight) to be booked." The "something" is vague.
        Vague Statement: "A number of server-side components (presumably Enterprise Java Beans, given the name used) implement something on a server computer. However, we don’t know what components exist, just that (presumably) there is a group of them." This highlights a lack of specificity in the example.
    Underlying Problem: Mixing functional structure, deployment, concurrency, software design constraints, and other concerns at different abstraction levels, relevant to different stakeholders.
    Notational Confusion: Overloading often leads to notational confusion due to the need to represent unrelated concepts on one diagram.
    Risk Reduction:
        Remove all elements from the Functional view except those related to functional elements, interfaces, and connectors.
        Create separate views for other architectural aspects (e.g., Deployment view).
        Develop views in parallel and cross-reference them to illustrate other aspects.

Diagrams without Element Definitions

    Issue: Tendency to draw structural diagrams (like functional structure models) without carefully defining the entities shown.
    Consequences: The model becomes meaningless without well-defined elements.
    Risk Reduction:
        Define each element as it is added to the model.
        Review definitions with stakeholders for clarity.

        This document excerpt discusses common challenges and risks associated with creating the Functional View of a software architecture.
Functional View: Challenges and Risk Reduction
Difficulty in Reconciling the Needs of Multiple Stakeholders

    Problem Description: The Functional View is of central interest to most stakeholders (end users, developers, system administrators, etc.), each with specific interests and needs. It is difficult to create a single view description or use a single model/notation suitable for all parties.
    Risk Reduction:
        Use different modeling languages for different stakeholders.
        Technical Stakeholders: Communicate using primary architectural models (e.g., functional structure model). Some notation explanation may be required.
        Nontechnical (Business) Stakeholders: Create simplified models derived from primary models. Less technical notations, such as sketches (described in Chapter 12) with brief textual annotation, are often more effective.

Wrong Level of Detail

    Problem Description: Defining too many layers of elements in the functional analysis process leads to designing all software rather than just architecturally significant parts. Conversely, insufficient detail risks misinterpretation and failure to deliver required qualities.
    Risk Reduction:
        Limit the level of detail: If defining more than two or three levels of elements (assuming 8-10 top-level functional elements), the level of detail may be too high.
        Avoid including details about the internal workings or structure of functional elements in the Functional View's models.
        For very large systems, model them as a group of systems rather than breaking down into individual elements to maintain tractability.

"God Elements"

    Problem Description: Similar to "God object" in object-oriented designs, a single, central, huge element ("Manager") can emerge in architecture descriptions, especially with overzealous consolidation. This element often embodies the entire program, with other elements acting as mere data structures.
        Leads to a complex, difficult-to-understand, and hard-to-maintain system.
        Characteristics of the "God element" dominate system quality properties, making it difficult to solve related problems (performance, reliability, scalability) as they all involve changing this one element.
    Diagram Hint (Figure 17-6, not provided in input, but referenced):
        The UML element diagram in Figure 17-6 illustrates structures that suggest a God element.
        Example: "Customer Management" system element in the example appears to exhibit major characteristics of a God element, with nearly all interelement interactions involving it. This suggests it contains too much functionality and has dependencies with too many other system elements.
        Proposed Solution: Repartition the system into elements with more evenly distributed functionality.
    Risk Reduction:
        Aim for a broadly even distribution of system-level responsibilities among major elements.
        Guideline: If more than 50% of system responsibilities are concentrated in less than 25% of functional elements, it may indicate large elements, lack of cohesion, development difficulty, and resistance to change.

Too Many Dependencies

    Problem Description: The converse of the "God object" problem, characterized by complex interactions and static object diagrams resembling "spiders fighting for control." This makes the system harder to design, build, change, and can lead to poor performance.
    Risk Reduction:
        This problem can be a symptom of too many small elements; judicious compression may help.
        General Guideline: A system element should only need to be aware of a couple of other elements to perform its functions.
        If any element needs to use services from more than 50% of other elements in the system, consider revising the functional structure.

Checklist (for Functional View)

    Fewer than 15 to 20 top-level elements?
    All elements have a name, clear responsibilities, and clearly defined interfaces?
    All element interactions occur via well-defined interfaces and connectors that link the interfaces?
    Elements exhibit an appropriate level of cohesion?
    Elements exhibit an appropriate level of coupling?
    Important usage scenarios identified and used to validate the system’s functional structure?
    Functional coverage of the architecture checked to ensure it meets functional requirements?
    Appropriate set of architectural design principles defined and documented, and does the architecture comply?
    Considered how the architecture is likely to cope with possible future change scenarios?
    Presentation of the view takes into account concerns and capabilities of all interested stakeholder groups? Will the view act as an effective communication vehicle for all these groups?

Further Reading (Relevant to Functional View Concepts)

    Clements et al. [CLEM03]: Detailed, thorough, practical guide to documenting various architectural styles. Discussions on overloading views and documenting interface styles are pertinent.
    Garland and Anthony [GARL03]: Describes designing software architecture for large-scale information systems. The approach for modeling message-oriented element interactions is derived from this book.
    Bass et al. [BASS03]: Techniques for element identification are based on architectural "unit operations" described more fully here.
    UML Tutorials:
        [FOWL03a]
        [MILE06]
    Rigorous Architectural Descriptions using UML:
        [CHEE01]
        [DSOU99]
    Rigorous Models (Timeless Book): [COOK94] (out of print, available in PDF from www.syntropy.co.uk/syntropy).
    Checkland [CHEC99]: Approach to understanding real user requirements using informal diagrammatic "rich picture" (analogous to sketches) for communication with end users.
    Meyer [MEYE00]: Definitive reference on Design by Contract.
    Mitchell and McKim [MITC02]: Concise, practitioner-oriented introduction to Design by Contract.
    Wirfs-Brock et al. [WIRF90]: One of the original books on responsibility-driven design.
    Wirfs-Brock [WIRF02]: Refinement to the responsibility-driven design approach.
    Shaw [SHAW94]: One of the first attempts to explain the importance of connectors between elements in models.