View Name: Software Architecture Concepts
2 Software Architecture Concepts

    Terminology Problem: The term "architecture" is inconsistently used across disciplines (building, naval) and within computer systems (microprocessors, networks, software programs).
    Purpose of Chapter: Defines and reviews core concepts: software architecture, architectural elements, stakeholders, and architectural descriptions.

Software Architecture

    Computer System Definition: Software elements (programs, libraries) and the necessary hardware (processors, memory, disks, network cards) to run them, specified/designed to meet requirements.
    Software Architecture Definition (ISO/IEC 42010): "The set of fundamental concepts or properties of the system in its environment, embodied in its elements, relationships, and the principles of its design and evolution."

System Elements and Relationships

    Elements: Pieces constituting a system (e.g., module, component, partition, subsystem). The term "elements" is preferred for its semantic neutrality, referring to architecturally significant pieces.
    Structures of Interest:
        Static Structure (Design-Time Elements):
            Definition: Defines internal design-time elements and their arrangement.
            Examples of Elements: Programs, object-oriented classes/packages, database stored procedures, services, self-contained code units, relational database entities/tables, data files, computers, disk, CPU, cables, routers, hubs.
            Relationships: Associations, relationships, or connectivity between elements (e.g., hierarchy of modules, dependencies, data item linkages, physical interconnections for hardware).
        Dynamic Structure (Runtime Elements):
            Definition: Defines runtime elements and their interactions.
            Examples of Interactions: Flows of information (element A sends messages to element B), parallel/sequential execution (element X invokes a routine on element Y), data effects (data item D created, updated, destroyed).
    Relationship between Static and Dynamic Structures: Closely related; static elements provide mechanisms for dynamic interactions. They are not the same (e.g., a single static client-facing element can have multiple runtime instances).

Fundamental System Properties

    Manifestation: Externally visible behavior and quality properties.
    Externally Visible Behavior:
        Definition: Defines functional interactions between the system and its environment.
        Interactions: Flows of information (in/out), system response to external stimuli, published "contract" or API.
        Modeling: Can treat system as a black box (request P yields response Q) or consider changes to internal state (request R causes creation of data item D).
    Quality Properties (Nonfunctional Characteristics):
        Definition: An externally visible, nonfunctional property such as performance, security, or scalability.
        Examples: Performance under load, peak throughput, information protection, likelihood of breaking, ease of management/maintenance/enhancement, usability for disabled persons.
        Relevance: Depends on circumstances, stakeholder concerns, and priorities.

Principles of Design and Evolution

    Consistency and Conventions: Well-structured, maintainable systems have consistent implementations and respect system-wide structuring conventions.
    Architectural Principle Definition: "A fundamental statement of belief, approach, or intent that guides the definition of your architecture." (Extended from Oxford English Dictionary definition).
    Benefits of Principles: Establishes a decision-making framework for consistent architecture, exposes underlying assumptions, useful for starting projects or resolving conflicts in requirements.

System Properties and Internal Organization (Example: Airline Reservation System)

    Example System: Airline Reservation System supporting transactions (booking, update, cancel, transfer, upgrade seats).
    Context Diagram (Figure 2-1):
        Rectangle: System ("Airline Reservation System").
        "Stick man": Customers (users of the system).
        Notation boxes: Additional supporting information.
    Externally Visible Behavior: Response to customer transactions (booking, updating, canceling).
    Quality Properties: Average response time under load, maximum throughput, system availability, time/skills/cost to repair defects.
    Candidate Architectures:
        Two-Tier Client/Server Approach (Figure 2-2):
            Static Structure: Client programs (presentation, business logic, database, network layers), central server (stores data in relational database), WAN connections.
            Dynamic Structure: Request/response model; requests submitted by client to server over WAN, responses returned. Static elements provide mechanisms for dynamic interactions.
            Potential Rationale for Choice: Operational simplicity, quick development, lower cost.
        Three-Tier Client/Server Approach (Figure 2-3):
            Static Structure: Client programs (presentation, network layers), application server (business logic, database, network layers), database server, connections.
            Dynamic Structure: Three-tier request/response model; client to application server over WAN, application server to database server (if needed), responses returned from application server to client.
            Potential Rationale for Choice: Better scalability, less powerful client hardware needed, better security.
        Candidate Architecture Definition: "A particular arrangement of static and dynamic structures that has the potential to exhibit the system’s required externally visible behaviors and quality properties."
        Comparison: Both must meet overall requirements (timely/efficient bookings). They share important externally visible behaviors and general quality properties, but differ in specific quality properties (e.g., maintainability vs. build cost).
        Analysis: Two-tier might support richer clients; three-tier might deliver better throughput/response time due to looser coupling.
        Architect's Role: Derive static/dynamic structures, understand exhibited behaviors/quality properties, select the "best" one.
    Relationship between Properties and Internal Organization:
        Externally Visible Behavior: Determined by combined functional behavior of internal elements.
        Quality Properties: Arise from quality properties of internal elements (overall quality is often limited by weakest element).
        Complexity Note: This distinction is a simplification; a non-scaling server can become functionally constrained.

The Importance of Software Architecture

    Intrinsic Property: Every computer system has an architecture, regardless of documentation or understanding.
    Principle: "Every system has an architecture, whether or not it is documented and understood."
    Uniqueness: Every system has precisely one architecture, though it can be represented in multiple ways.

Architectural Elements

    Definition: "A fundamental piece from which a system can be considered to be constructed."
    Nature: Depends on system type and context (e.g., programming libraries, subsystems, deployable software units, reusable products, entire applications).
    Key Attributes:
        Clearly defined set of responsibilities.
        Clearly defined boundary.
        Clearly defined interfaces (services provided to other elements).
    Terminology Avoidance: "Component" and "module" are avoided due to existing specific meanings (programming-level component models, programming language constructs). "Element" is used consistently, following ISO 42010 and Bass, Clements, and Kazman [BASS03].

Stakeholders

    Beyond Users: People affected by a software system extend beyond users (e.g., those who build, test, operate, repair, enhance, pay for it). Each group has its own requirements and interests.
    Definition: "An individual, team, organization, or classes thereof, having an interest in the realization of the system." (Based on ISO Standard 42010).

Individual, Team, or Organization

    Broad Scope: Interests extend beyond developers and users to supporters, deployers, and financiers.
    Architect's Role: Engage stakeholders, persuade them of involvement's importance, and obtain commitment.
    Representative Selection: If not all members of a class (e.g., all users) can be captured, select representative stakeholders.

Interests and Concerns

    "Having an interest": Broad criterion, interpretation specific to individual projects. Stakeholders may not know precise requirements early on.
    Concern Definition: "A requirement, an objective, a constraint, an intention, or an aspiration a stakeholder has for that architecture."
    Conflict: Many concerns are common, but some are distinct and may conflict. Resolving conflicts is a significant challenge.
    Example: The Quality Triangle (Figure 2-4):
        Corners: Cost, Quality, Time to Market.
        Ideal vs. Reality: Cannot have high quality, zero cost, and immediate delivery simultaneously.
        Compromises: Achieving two out of three is more realistic.
        Indicative Combinations:
            High quality: Expensive, longer time to market.
            Low cost: Inexpensive, lower quality, longer time to market (implied).
            Short time to market: More expensive, moderate quality, moderate time to market (implied).
        Architect's Job: Understand which attributes are important to which stakeholders and reach acceptable compromises.

The Importance of Stakeholders

    Driving Force: Stakeholders drive the architecture's shape and direction; it's created solely for their benefit and needs.
    Decision-Makers: Ultimately make or direct fundamental decisions on scope, functionality, operational characteristics, and structure.
    Principle: "Architectures are created solely to meet stakeholder needs."
    Evaluation Criterion: System must adequately meet stakeholder needs to be considered a success, regardless of good architectural practice. Architectures must be evaluated against stakeholder needs and abstract architectural/software engineering principles.
    Conflict Resolution: Architect often balances conflicting stakeholder needs (e.g., accepting higher maintenance costs for performance-critical systems).
    Principle: "A good architecture is one that successfully addresses the concerns of its stakeholders and, when those concerns are in conflict, balances them in a way that is acceptable to the stakeholders."

Architectural Descriptions

    Purpose: To describe the complexity of a software system's architecture to those who need to understand it.
    Definition: "A set of products that documents an architecture in a way its stakeholders can understand and demonstrates that the architecture has met their concerns."

    Chapter 2: Software Architecture Concepts

    Products of Architecture: Include architectural models, scope definitions, constraints, and principles.
    Architectural Description (AD):
        Presents both the essence and detail of an architecture.
        Provides an overall picture while decomposing into enough detail for validation and system building.
        A good AD is understandable to stakeholders and demonstrates that their concerns have been met.
        Contains all necessary information to communicate the architecture effectively.
        Principle: Not every system has an effectively communicated architecture, even though every system has one.
        Example: An AD for an airline reservation system should consider quality properties like response time and system reliability.
        The architect is a major user of the AD, but all stakeholders need to understand the architecture to varying degrees.
        Principle: A good AD effectively communicates the key aspects of the architecture to the appropriate stakeholders.
        Choosing the right techniques, models, and languages to document architectures is crucial.
    Relationships Between Core Concepts (Figure 2–5):
        A system is built to address stakeholder needs, concerns, goals, and objectives.
        The architecture comprises architectural elements and their interelement relationships.
        An AD documents an architecture for its stakeholders and demonstrates that it has met their needs.
    Summary:
        The architecture defines a system's static structure, dynamic structure, externally visible behavior, quality properties, and design principles.
        A candidate architecture has the potential to exhibit required behaviors and quality properties.
        An architectural element is a clearly identifiable, meaningful piece of a system.
        A stakeholder is a person or group with an interest in the architecture.
        An AD documents an architecture in a way that stakeholders can understand.
    Further Reading:
        ISO/IEC Standard 42010 addresses the creation, analysis, and sustainment of architectures.
        The Software Engineering Institute's work provides a thorough introduction to software architecture.
        Original works by Shaw and Garlan, and Perry and Wolf, offer minimalist introductions to fundamental ideas.
        The Art of Systems Architecting introduces architecture as principles and techniques across complex systems domains.
        Just Enough Software Architecture guides risk-driven architecting.
        Essential Software Architecture is a concise introduction to important topics.
        The Process of Software Architecting defines a formal process for software architecture work.

Chapter 3: Viewpoints and Views

    Challenges in Describing Architecture:
        Identifying main functional elements and their interactions.
        Managing, storing, and presenting information.
        Defining physical hardware and software elements.
        Providing operational features, development, test, support, and training environments.
        Avoid: Using a single, heavily overloaded model.
        Example: A single diagram attempting to represent all aspects of an airline reservation system failed to engage stakeholders due to complexity and missing details.
        Principle: It is not possible to capture all features and properties of a complex system in a single comprehensible model.
    Architectural Views:
        Partitioning the AD into separate but interrelated views, each describing a separate aspect of the architecture.
        Analogy: Architectural drawings or scale models of a building.
        Strategy: Describe a complex system using a set of interrelated views.
    Definition of an Architectural View:
        A way to portray aspects of the architecture relevant to specific concerns and stakeholders.
        Based on the work of Parnas, Perry, Wolf, and Kruchten's "4+1" View Model.
        Definition: A view represents structural aspects of an architecture, illustrating how it addresses stakeholder concerns.
        Considerations for Including in a View:
            View scope: What structural aspects are being represented?
            Element types: What types of architectural elements are being categorized?
            Audience: What class of stakeholder is the view aimed at?
            Audience expertise: How much technical understanding do stakeholders have?
            Scope of concerns: What concerns is the view intended to address?
            Level of detail: How much do stakeholders need to know about this aspect?
        Strategy: Include only information that helps explain the architecture to stakeholders or demonstrates that system goals are being met.
    Viewpoints:
        Templates and patterns for creating architectural views.
        Definition: A viewpoint is a collection of patterns, templates, and conventions for constructing one type of view.
        Provide a framework for capturing reusable architectural knowledge.
        Analogy: Viewpoints are like classes, and views are like objects in object-oriented development.
        Aim to bring structure and consistency to architecture descriptions.
        Strategy: Be clear about the concerns, architectural elements, and target audience of a view.
    Relationships Between Core Concepts (Figure 3–1):
        A viewpoint defines the aims, audience, and content of a class of views.
        A view conforms to a viewpoint and communicates the resolution of concerns.
        An AD comprises a number of views.
    Benefits of Using Viewpoints and Views:
        Separation of concerns: Helps in design, analysis, and communication.
        Communication with stakeholder groups: Tailored views for different stakeholders.
        Management of complexity: Focus on each aspect separately.
        Improved developer focus: Helps ensure the right system gets built.
    Pitfalls:
        Inconsistency: Potential issue when using multiple views.

Functional View

The Functional Viewpoint describes the system’s runtime functional elements, their responsibilities, interfaces, and primary interactions. This view is a cornerstone of most Architecture Description (AD) documents and is often the first part stakeholders read. It influences the shape of other system structures, including information, concurrency, and deployment. This view also significantly impacts the system’s quality properties such as evolvability, security, and runtime performance.
Relationship to Other Viewpoints

    Defines System Functionality: Along with the Information and Concurrency viewpoints, it defines how the system provides its functionality.
    Drives Other Structures: The Functional View drives the shape of other system structures, such as the information structure, concurrency structure, and deployment structure.
    Influences Development View: The Development viewpoint defines standards and models for the construction of the architecture’s functional elements.

Quality Properties and Architectural Perspectives

The Functional View is affected by various architectural perspectives, which address quality properties across multiple architectural views.
Security Perspective

From the Functional viewpoint, security requires:

    Ability to identify and authenticate users (internal, external, human, mechanical).
    Effective yet unobtrusive security processes.
    Resilience of external processes to attacks.
    Example: Protection of access via login screens requiring credentials. Operational staff must be able to manage user lists and reset passwords.

Performance and Scalability Perspective

When the Performance perspective is applied to the Functional view, it guides the design to ensure appropriate performance.
Evolution Perspective

When the Evolution perspective is applied to the Functional view, it guides the design to consider required changes and build in appropriate flexibility.
Key Considerations

    Complexity Management: Capturing the entirety of a complex system's architecture in a single model is not feasible. The use of multiple views, including the Functional View, helps manage this complexity by focusing on specific aspects.
    Consistency: Achieving cross-view consistency is a manual process. A checklist (Chapter 23, not included in this extract) assists in ensuring consistency between standard viewpoints.
    Avoiding Fragmentation: To minimize effort and avoid fragmentation, views that do not address significant concerns should be eliminated. Hybrid views (e.g., combined deployment and concurrency) can be considered, but care must be taken to ensure they remain understandable and maintainable.

# Perspectives and Views in Software Architecture

## Applying Perspectives to Views

The document describes a framework for applying **architectural perspectives** to **architectural views**. This process helps ensure a system meets its non-functional requirements.

### The Grid for View and Perspective Application

A grid can be used to record which perspectives are applied to which views (referencing Figure 4–2, not provided). This grid can also be detailed to indicate the importance of each perspective to each view for a specific system (referencing Table 4–1, provided below).

### Impact of Applying Perspectives

Applying a perspective to a view typically results in modifications to existing views rather than the creation of new, dedicated views for that perspective.

**Example: Applying the Security Perspective**

If the **Security perspective** is applied to a candidate architecture:
* **Functional View:** The system might be partitioned differently to restrict access to certain parts.
* **Deployment View:** New hardware and software elements (e.g., encryption components, access control mechanisms) may be added to define their placement.
* **Development View:** Updates might be needed to define how new security-related elements should be used.
* **Operational View:** New or modified operational procedures (e.g., certificate management, handling sensitive data backups) may be introduced.

### Outcomes of Applying a Perspective

Applying a perspective can lead to **insights, improvements**, and **artifacts**.

#### Insights

* Often leads to the creation of a model demonstrating the system's ability to meet a required quality property.
* Helps identify deficiencies in the architecture (e.g., unaddressed security threats).
* Drives further architectural design activity and should be recorded as rationales for design decisions.

#### Improvements

* If the architecture is found to be deficient, existing models in a view may need changes, or additional models may be created.
* **Example:** Applying the **Performance and Scalability perspective** to the **Deployment view** might show a need to replicate application servers for scaling, leading to changes in the Deployment model (e.g., showing multiple servers) and potentially the Functional or Information views for load balancing support.
* These improvements are integral to the Architectural Description (AD).

#### Artifacts

* Some outputs of applying a perspective are of lasting value and should be preserved.
* These are typically documents, models, or implementations referenced from the AD as supporting information.
* **Example:** Applying the **Location perspective** to the **Deployment view** might produce a spreadsheet modeling the physical network to verify bandwidth and capacity. This spreadsheet should be retained and referenced from the AD.

## Relationships Between Core Concepts

(Referencing Figure 4–3, not provided, which updates Figure 3–1 with perspective relationships)

* The content of a **view** can be shaped by multiple **perspectives** to ensure the system exhibits desired quality properties.
* A **perspective** addresses various **concerns** of the system's stakeholders.

### Benefits of Using Perspectives

* **Defines Concerns:** Guides architectural decision-making to ensure the architecture exhibits specific quality properties (e.g., **Performance perspective** defines concerns like response time, throughput, predictability).
* **Provides Conventions:** Offers common conventions, measurements, notation, or language to describe system qualities (e.g., **Performance perspective** defines standardized measures like response time, throughput, latency).
* **Validation Guidance:** Describes how to validate the architecture across views to meet requirements (e.g., **Performance perspective** describes mathematical models, simulations, prototyping, and benchmarking).
* **Offers Solutions:** Provides recognized solutions to common problems, facilitating knowledge sharing (e.g., **Performance perspective** describes multiplexing hardware devices for throughput improvement).
* **Systematic Approach:** Helps organize work and ensures concerns are systematically addressed.

---

## Perspective Pitfalls

* **Conflicting Solutions:** Solutions suggested by different perspectives may conflict (e.g., evolvability versus performance). Architects must balance these competing needs.
* **Varying Relevance:** The degree to which each perspective is considered varies significantly based on stakeholder concerns and priorities.
* **Contextual Application:** Perspectives offer general advice; it's crucial to consider the advice's relevance to the specific situation and apply it appropriately.

---

## Comparing Perspectives to Viewpoints

The document clarifies the distinction between viewpoints and perspectives, a common point of confusion since the first edition.

* **ISO 42010 (formerly IEEE 1471):** This standard addresses cross-cutting concerns by sharing models across architecture views, which it states can be used to express architectural perspectives. While this approach is workable and compatible, the document argues for treating perspectives as a distinct concept.

### Key Differences

* **Focus:**
    * **Viewpoints:** Focus on guiding the production of models that describe the architecture.
    * **Perspectives:** Focus on providing activities and tactics to ensure the system exhibits required quality properties.
* **Outcome:**
    * Using a **viewpoint** guides the creation of a particular type of view.
    * Using a **perspective** usually results in changes to existing architectural views (system structures) rather than creating new structures.
* **Purpose:** Perspectives also capture common problems, pitfalls, and identify solutions.

### Summary of View, Viewpoint, and Perspective

* **View:** A representation of all or part of an architecture, documenting architecturally significant features according to related concerns. It captures one or more architectural structures and forms part of the AD.
* **Viewpoint:** Guides the process of creating a specific type of view. It defines the concerns addressed and the approach for describing that architectural aspect.
* **Perspective:** Guides the design process to ensure the system exhibits one or more important qualities. Analogous to a viewpoint for quality properties, but primarily causes changes to existing architectural views. Also used for capturing problems/solutions.

The document concludes that using a distinct concept for architectural perspectives, separate from viewpoints, offers advantages for handling quality properties.

---

## Our Perspective Catalog

Part IV of the book defines several perspectives for large-scale information systems (referencing Table 4–2, provided below).

* Not every perspective is relevant to every system and view.
* It is rare to consider the complete set of perspectives for anything other than the largest and most complex projects.

**Strategy:** Apply only the most relevant perspectives to your views, based on stakeholder needs, quality property importance, and judgment.

### Table 4–2: Perspective Catalog (Incomplete: Missing full descriptions from table)

| Perspective             | Desired Quality                                                                                                                                                                                                                         |
| :---------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Accessibility** | The ability of the system to be used by people with disabilities.                                                                                                                                                                       |
| **Availability and Resilience** | The ability of the system to be fully or partly operational as and when required and to effectively handle failures that could affect system availability.                                                                      |
| **Development Resource** | The ability of the system to be designed, built, deployed, and operated within known constraints related to people, budget, time, and materials.                                                                                     |
| **Evolution** | The ability of the system to be flexible in the face of the inevitable change that all systems experience after deployment, balanced against the costs of providing such flexibility.                                                  |
| **Internationalization** | The ability of the system to be independent from any particular language, country, or cultural group.                                                                                                                                     |
| **Location** | The ability of the system to overcome problems brought about by the absolute location of its elements and the distances between them.                                                                                                    |
| **Performance and Scalability** | The ability of the system to predictably execute within its mandated performance profile and to handle increased processing volumes in the future if required.                                                                    |
| **Regulation** | The ability of the system to conform to local and international laws, quasi-legal regulations, company policies, and other rules and standards.                                                                                         |
| **Security** | The ability of the system to reliably control, monitor, and audit who can perform what actions on which resources and the ability to detect and recover from security breaches.                                                       |
| **Usability** | The ease with which people who interact with the system can work effectively.                                                                                                                                                           |

### Table 4–3: Most Important Perspectives for Typical System Types (Incomplete: Missing full table details, only columns)

| System Type Perspective | OLTP   | Calculation High-Volume System | DSS/MIS Enterprise System | Service/Web Site Middleware | Package System |
| :---------------------- | :----- | :----------------------------- | :------------------------ | :-------------------------- | :------------- |
| Accessibility           | Varies | Low                            | Varies                    | High                        | High           |
| Availability and Resilience | Varies | High                           | Medium                    | High                        | Medium         |
| Development Resource    | Medium | High                           | Medium                    | High                        | Low            |
| Evolution               | Varies | Low                            | High                      | Varies                      | Medium         |
| Internationalization    | Varies | Low                            | Varies                    | High                        | Varies         |
| Location                | Varies | Low                            | Low                       | High                        | Varies         |
| Performance and Scalability | Varies | High                           | Varies                    | High                        | Varies         |
| Regulation              | Varies | Low                            | Varies                    | Varies                      | Varies         |
| Security                | Varies | Low                            | Medium                    | High                        | High           |
| Usability               | Medium | Low                            | Low                       | High                        | Medium         |

---

The Role of the Software Architect
Definition of Architecture Definition Process

Architecture definition is a process that involves:

    Capturing stakeholder needs and concerns.
    Designing an architecture to meet these needs.
    Clearly and unambiguously describing the architecture via an architectural description (AD).

The goal is to design an architecture that meets stakeholder needs. This includes:

    Capturing stakeholder needs: Understanding what is important to stakeholders, potentially reconciling conflicts (e.g., functionality vs. cost), and recording and agreeing on these needs.
    Making a series of architectural design decisions that result in a candidate architecture.
    Assessing the candidate architecture to determine how well it meets stakeholder needs.
    Refining the architecture until it is adequate.
    Capturing the architectural design decisions and resulting architectural structures in an AD appropriate to the environment.

These activities are normally performed iteratively.

Principle: A good architecture definition process leads to a good architecture, documented by an effective AD, which can be realized in a time-efficient and cost-effective manner.
Architecture Definition, Requirements Analysis, and Software Design

Architecture definition is distinct from both design and requirements analysis, acting as a bridge between the problem space and the solution space (as illustrated in Figure 5–1, which is not provided in text but referenced as a diagram hint).

    Design:
        Focused on the solution space.
        Targeted primarily at developers.
        Works within defined constraints (system's requirements).
        Translates requirements into specifications for a conformant system.
        Historically, less focus on needs of operations or support.

    Requirements Analysis:
        Focused on the problem space.
        Defines what is desired rather than what is possible.
        Works within defined constraints (system’s required scope), with more freedom than design.

Key differences of Architecture Definition:

    Takes input from a much wider range of people than just the user community (i.e., stakeholders).
    Considers a much wider range of concerns than just functionality (i.e., views and perspectives).
    Considers the big picture as well as the details.
    Often a process of discovery rather than just capture, especially in early stages when stakeholder expectations may be hazy, ideas conflicting, or technical knowledge lacking.
    Manages the practical reality that stakeholders often think about technology solutions from day one.

Boundary between Requirements Analysis and Architecture Definition

The architect is involved in analyzing, understanding, and prioritizing system requirements, and assessing implementation difficulty.

    Architect's role does not strictly include requirements gathering. Ideally, a complete, consistent, prioritized list of key goals and requirements is provided.
    Requirements analysts often struggle to trade off requirements without insight into implementation costs and risks.
    Architects provide insight into implementation options to help understand the cost of providing each requirement.

Strategy: Work with requirements analysts to understand system requirements and their relative importance. For each important requirement, consider the likely difficulty of implementing it and provide feedback to help them understand what can and cannot be achieved.
Boundary between Architecture Definition and Design

A key decision for an architect is determining if something is architecturally significant.

Definition: A concern, problem, or system element is architecturally significant if it has a wide impact on the structure of the system or on its important quality properties (e.g., performance, scalability, security, reliability, evolvability).

    Predicting architectural significance is difficult and requires judgment, skill, and expertise.
    Context matters: New technologies may make reliability and performance very significant, unlike established technologies.
    The architect's job is to focus attention on decisions likely to significantly affect the system's ability to meet its goals.
    Architectural concerns are not only abstract; "the devil is in the details." Aspects of the architecture must be considered at all levels, from strategy to code.
    The scope should be continually reviewed as the architecture develops.

Example: Database Design Significance

    Generally not architecturally significant: In systems with simple data access patterns, detailed database schema might not impact quality goals.
    Architecturally significant: In systems with extensive, complex database usage (e.g., many large, performance-critical queries), detailed database design decisions can have serious ramifications for performance and stability.
    When considering significance, look ahead to whether different options will impact key qualities. If options are likely to cause future trouble, it's architecturally significant.

Strategy: As you design the architecture, review what has been determined as architecturally significant (or not) and revise as necessary based on deeper understanding of stakeholder concerns and the architecture.
The Role of the Architect

Principle: The architect is responsible for designing, documenting, and leading the construction of a system that meets the needs of all its stakeholders.

Four aspects of this role:

    Identify and engage stakeholders.
    Understand and capture stakeholder concerns.
    Create and own the definition of an architecture that addresses these concerns.
    Take a leading role in the realization of the architecture into a physical product or system.

The architect "owns the big picture," developing and maintaining a high-level view to guide detailed design, coding, testing, and deployment. The architect must select a fit-for-purpose architecture and document it appropriately.

Architect's Involvement During the Software Development Lifecycle (Figure 5–2 referenced for diagram hints):

    Initial phases: Intense involvement in defining scope, validating requirements, and providing technical leadership to shape the architecture.
    Design, Build, Test phases: Involvement typically lessens. The architect may act as a design authority or designer, involved in mentoring, reviews, problem resolution, and technical leadership. The architect leads any necessary architectural changes.
    Prior to and during Acceptance: Involvement peaks again, providing support and guidance for last-minute problems and ensuring a smooth transition into the operational environment.

Strategy: Stay involved with the development process beyond AD creation, through construction, acceptance, and handover (possibly at a reduced level of involvement).
Architectural Leadership

"Architect" is often a technology leadership role. From a system standpoint, this includes people-focused activities to ensure successful implementation:

    Explaining and promoting the architecture to business and technology stakeholders, justifying principles and decisions.
    Providing input to and support for planning and estimating tasks.
    Participating in change control processes.
    Taking responsibility for and signing off on technical milestones.
    Helping to resolve issues during development.
    Taking on more specific development roles (e.g., design authority).
    Reviewing documentation and possibly code.

Many architects also help develop and promote the practice of architecture within the organization:

    Arranging/delivering architectural training.
    Mentoring junior staff (e.g., in design roles).
    Developing viewpoints for the organization.
    Developing and overseeing architectural governance processes (e.g., architectural reviews).

The extent of these responsibilities depends on project characteristics, skills, and aspirations.
Interrelationships Between Core Concepts (Figure 5–3 referenced for diagram hints)

Key relationships augmenting the model:

    The architect captures and consolidates the concerns of the stakeholders.
    The architect designs an architecture that meets these concerns.
    The architect creates and owns the architectural description (AD).
    An architecture definition process guides the definition of the architecture.
    The architect follows the architecture definition process to carry out these tasks.

Specializations of Architects

Architects may specialize, especially on large projects with architecture teams. The core concepts (stakeholders, views, principles, models) apply within their scope. Common specializations:

    Product architect: Responsible for delivery of one or more releases of a software product to external customers, overseeing its technical integrity. Often stays associated with the product over multiple release cycles. Challenge: identifying user stakeholders before the first release.
    Domain architect: Focuses on a particular domain (e.g., business architecture, data architecture, network architecture). Valuable for large, complex, groundbreaking systems or filling knowledge gaps.
    Infrastructure architect: Owns the provision of hardware and software infrastructure, often company-wide. Includes data centers, servers, storage, networking, peripherals, and infrastructure software (e.g., enterprise security, databases, messaging, identity).
    Solution architect: Takes a broad, high-level view of the entire solution, focusing on wider issues beyond technology, such as business process change, procurement, and staffing.
    Enterprise architect: Responsible for the cross-system information systems architecture of the entire enterprise (e.g., sales, marketing, client-facing systems, supply chain, HR). Concerned with company-wide principles, standards, policies, and business process change.

The Organizational Context: Architect vs. Other Roles
Business Analysts

    Responsible for capturing and documenting detailed business requirements, primarily from user community stakeholders.
    Ensures requirements are correct, complete, and consistent.
    Architects often draw on their specialized knowledge, especially for views of interest to acquirers, users, and assessors.

Project Managers

    Responsible for ensuring product/system delivery and meeting commercial priorities (resources, costs, timescales).
    Architects help develop plans and assess their reasonableness.
    Architects provide technical information, feedback, advice, and risk assessment throughout the project lifecycle.
    Productive relationship: Project manager focuses on stakeholders, plans, budgets, staffing, milestones, deadlines, deliverables; architect focuses on stakeholders, concerns, scope, requirements, views, models.

Design Authorities (Technical Design Authority, Technical Lead)

    Takes overall responsibility for the quality of internal element designs.
    Architects often fill this role as the project moves into the design phase.
    Takes architectural views as input and guides/leads software developers.
    Often the primary technical contact for implementation details and underlying technology.
    Architectural vs. Design Authority distinction:
        Architect: Responsible for decisions with significant impact on important stakeholders or requiring tradeoffs between stakeholder needs.
        Design Authority: Responsible for decisions visible only within the development team (internal system decisions).
    Cooperation is essential between roles.

Example 1: Relational Database Version Selection (Architectural Decision)

    Scenario: Choosing between current and new version of a relational database for persistent storage. New version offers features/performance but carries commercial risk (skills, platform confidence, point-zero release issues).
    Reasoning: Because of potential commercial impact, the architect should be involved.

Example 2: Database Performance Tuning (Design Authority Decision)

    Scenario: End-user query performance issues due to database structure, indexes, and object distribution. Changes involve internal restructuring, not affecting data access via stored procedures (only making it faster).
    Reasoning: Changes have no visible stakeholder impact (other than compliance). These are internal system decisions, suitable for the design authority.

Technology Specialists

    Provides detailed expertise in one specific area.
    Architect provides breadth, technology specialist provides depth.
    Responsibilities: Provide detailed facts, assess architectural technical feasibility, spot pitfalls early.
    Architects should apply information from specialists to solve problems.
    Architects are not expected to know everything.

Principle: The architect provides and oversees the architectural breadth and works closely with both business-focused and technology-focused specialists who provide the specialist depth.
Developers

    Architect's involvement continues beyond AD handover.
    During build and test phases, the architect maintains a technology leadership role to ensure adherence to the AD.

Architect's Role and Responsibilities View
Architect's General Involvement and Mentoring

    Mentoring: Mentoring staff through the detailed design process.
    Design Review: Reviewing designs to ensure conformance to architectural principles.
    Arbitration: Arbitrating technology disputes.
    Development: Potentially developing pieces of the implementation.
    Testing: Involvement in integration and system testing to ensure appropriate functional and operational characteristics are exercised.
    Change Leadership: Leading the change process for Architecture Description (AD) modifications during development.
    Interaction with Development Team: Nature of interaction depends on the lifecycle model (e.g., Waterfall vs. iterative/agile).

Architect's Skills

    Technology Focus (Traditional): Strong technology background.
    Broad Technology Understanding: Across-the-board, high-level understanding of technology.
    Problem-Solving: Understanding real-world issues and problems the system solves.
    System Design & Building Experience: Real experience with designing and building systems.
    Deep Technical Expertise: Typically one or more areas of deeper technical expertise, providing ability to recognize good design.
    Business Domain Understanding:
        Understanding main business processes and main types of information.
        Understanding dependencies, importance, and criticality of business processes and information.
        Enables effective communication with business stakeholders and informed prioritization/tradeoff decisions.
    Soft Skills:
        Information Capture:
            Capturing diverse information from various stakeholders with different interests and expertise levels.
            Keeping stakeholders on track in interviews, focusing on architectural concerns, drilling down into detail.
            Listening and taking notes simultaneously.
        Facilitation:
            Managing workshops and meetings effectively for information capture and solution mapping.
            Handling mixes of senior/junior stakeholders or hidden/explicit conflict.
        Negotiation:
            Reaching consensus among diverse stakeholders with conflicting/incompatible concerns.
            Understanding and acting on what is truly valuable and what can be conceded.
        Communication:
            Effectively communicating the architecture to all stakeholders for buy-in.
            Tailoring communication methods (in-person, documents, concise, detailed) to different stakeholder interests.
        Flexibility:
            Rapidly learning unfamiliar business areas and technologies.
            Making quick changes of direction.
            Discarding preconceived ideas about problems or solutions.
            Knowing when to hold ground.
    Confidence Building: Earning and maintaining confidence of all stakeholders (senior management, users, developers, third parties, operational staff).

Architect's Responsibilities (Pro Forma List)

    Ensure that the scope, context, and constraints are documented and accepted.
    Identify, engage, and enfranchise stakeholders.
    Facilitate system-level decisions, ensuring they are based on the best information and aligned with stakeholder needs.
    Arbitrate and ensure consensus when stakeholder needs are in conflict or incompatible.
    Arbitrate and ensure consensus when architectural compromises are necessary (e.g., performance vs. flexibility, security vs. ease of use).
    Capture and interpret input from technical and domain specialists, representing it accurately to stakeholders.
    Define and document the architecture of the system.
    Define and document strategies, standards, and guidelines for system build and deployment.
    Ensure the architecture meets system quality attributes.
    Develop and own the Architecture Description (AD), managing all changes to it.
    Help ensure that agreed-upon architectural principles and standards are applied to the finished system or product.
    Provide technical leadership.

Role Definition and Terms of Reference

    Rarity of Formal Role Definition: It is rare for a formal architect role definition to exist in many organizations.
    Creating a Role Definition: Helpful to create one if it doesn't exist, using the provided list as a template.
    Contents of Role Definition:
        Architectural scope (tasks performed).
        Deliverables (documents and other material produced).
        Working methodology (e.g., conducting architectural reviews).
    Strategy: Ensure clear terms of reference for the role on any project; if not existing, draw up a brief document and agree with stakeholders.
    Broader Organizational Responsibilities:
        Developing and promoting the role of architecture within the organization.
        Defining viewpoints.
        Developing architectural processes, tools, templates, and other materials.

Key Concepts from Chapter (Summary of Part I)

    Architecture Definition: Process of capturing stakeholder needs/concerns, designing an architecture to meet them, and fully/unambiguously describing it via an AD.
    The Architect: Person or group responsible for designing, documenting, and leading the construction of an architecture that meets all stakeholder needs.
    Role Complexity: No single commonly accepted definition; includes elements of requirements capture and high-level design, but is more than either.
    Four Main Responsibilities (Reiterated):
        Identify and engage stakeholders.
        Understand and capture their concerns.
        Create and take ownership of the AD.
        Take a leading role in the realization of the architecture.
    Architectural Specializations (Mentioned): Product architects, domain architects, infrastructure architects, solution architects, enterprise architects.
    Comparison to Other Roles: Architect vs. business analysts, project managers, design authorities, technology specialists, developers.
    Importance of Architect: Primarily during early stages of system development and during acceptance; lesser role during build and test phases.
    Skills and Responsibilities: Discussion of required skills and a list of responsibilities.

Further Reading (References for external knowledge)

    [CLEM10] - General architecture books containing discussion of the architect's role.
    [MCGO04] - Discussion of roles related to software and enterprise architecture.
    [FISH03], [PELL09], [BREN10] - Books for developing soft skills (information capture, communication).
    [KRUC03] - Definition of "architecturally significant."