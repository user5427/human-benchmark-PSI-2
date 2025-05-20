The Information Viewpoint
Definition

Describes the way that the system stores, manipulates, manages, and distributes information.
Concerns

    Information structure and content
    Information purpose and usage
    Information ownership
    Enterprise-owned information
    Identifiers and mappings
    Volatility of information semantics
    Information storage models
    Information flow
    Information consistency
    Information quality
    Timeliness, latency, and age
    Archiving and information retention

Models

    Static information structure models
    Information flow models
    Information lifecycle models
    Information ownership models
    Information quality analysis
    Metadata models
    Volumetric models

Problems and Pitfalls

    Representation incompatibilities
    Unavoidable multiple updaters
    Key-matching deficiencies
    Interface complexity
    Overloaded central database
    Inconsistent distributed databases
    Poor information quality
    Excessive information latency
    Inadequate volumetrics

Stakeholders

Primarily users, acquirers, developers, testers, and maintainers, but most stakeholders have some level of interest.
Applicability

Any system that has more than trivial information management needs.
Purpose of Information Systems

The ultimate purpose of any information system is to manipulate data in some form. This data may be:

    Stored persistently in a database management system, ordinary files, or other storage media (e.g., flash memory).
    Transiently manipulated in memory while a program executes.

Architect's Role in Data Modeling

As an architect, data modeling should be performed at an architecturally significant level of detail. Focus on aspects of the data model where errors would affect the system as a whole, not just a part of it.
The objective is to develop a summary view of:

    Static information structure
    Dynamic information flow This aims to answer architecturally significant questions regarding ownership, latency, relationships, identifiers, and so forth.

Information Structure and Content

The structure and content of the information managed by the system are significant concerns. The architect's challenge is to focus on the most important aspects of information structure that have system-wide impact, deferring detailed modeling to data modelers and designers.
Key Data Items to Focus On

Focus on a relatively small number of data items (entities, classes) and their relationships. Selection depends on problems being solved and stakeholder concerns.
Consider the following when selecting data items of interest:

    Core to Primary Responsibilities: Focus on data items central to the system's primary functions or those stakeholders (primarily users, but also maintainers) view as particularly significant or meaningful.
    Information-Rich Data Items: Choose data items with many attributes, rather than type entities.
        Fundamental to the nature of concerns being addressed.
        Significant to users or other stakeholders.
        Complex or poorly understood internal structure.
        Potentially significant impact on the system’s quality properties depending on representation.
        Heavily used or volatile (contents change frequently).
    Abstraction in Early Stages:
        Focus on abstract rather than physical information.
        Keep models simple.
        Do not prioritize formal modeling techniques (e.g., relational normalization) at this point.
        Early models should align with and be driven by system functionality.
        Less concern with physical considerations like location or ownership initially.

Information Purpose and Usage

Information can be used in different ways, and the distinction in usage patterns is important for system design, as they often imply different ownership rules and architectural solutions.
Common Information Usage Patterns:

    Transaction Store (OLTP Database):
        Manages information for day-to-day operational business processes.
        Highly volatile information.
        Needs to process a large number of concurrent read/write operations with short latency and high reliability.
    Reporting Database:
        Implemented to service large, complex queries that would otherwise strain the transaction store.
        Fed in batch or real-time from the transaction store.
        Essentially read-only (apart from incoming feeds).
        Optimized for complex ad hoc queries rather than updates, often with many indexes and significant denormalization.
    Data Warehouse (OLAP Data Store):
        Manages historical information for analysis and trend discovery.
        Holds records of activity going back many years.
        Can feed into more specialized data marts (managing information from specific domains or time periods).
        Used to retrieve specific historical information or analyze trends over time.
    Reference Data (Static, Master, Lookup Data):
        Information on people, places, and things that categorizes or classifies transactional information.
        Includes business entities like calendars, customers, products, prices, locations, employees, external organizations, and "type" information (e.g., product type).
        Fairly static, changing infrequently.
        Significantly less volume compared to transactional and operational information.
        May not be owned by the system, posing an architectural challenge.

Architectural Considerations for Future Growth:

Even if distinctions in information usage are not critical initially, accounting for potential partitioning, differing store speeds, data duplication between stores, and other impacts in the initial architectural design will facilitate easier future separation of reporting databases, data warehouses, or enterprise data stores as data volumes grow.
Information Ownership

Architectures involving integration of new/existing systems often have information physically distributed across multiple data stores, leading to various problems.
Problems Arising from Distributed Information:

    Identifying the most up-to-date copy of a data item.
    Synchronizing information held in multiple locations.
    Handling information derived from data managed and owned elsewhere (e.g., account balances from activity).
    Determining appropriate validation and business logic for data modification, and assumptions about data validated elsewhere.
    Reconciling conflicts when the same data item can be modified in several places.

Example: Insurance Company Data Synchronization Issues

    Scenario: An insurance company with a central customer database. Salespeople download extracts to laptops, upload sales data later. A call center allows customers to update details and sell products.
    Problems:
        Laptops overwrite more recent central data, and vice versa.
        Updates to central database rejected due to more stringent validation rules.
    Architectural Strategy (Incomplete/Vague): The architect must agree with business stakeholders on rules for update conflicts and failures (e.g., recent updates override older ones). These rules are then coded into central system and laptop applications. (Clarification needed: Specific rule examples, how "more stringent validation" is addressed).

Information Ownership Model

A useful way to analyze and address these problems is to develop a model of information ownership.

    Information Owner (or Master): The system or data store containing the definitive, up-to-date, validated value of a data item.
    The information owner always has the correct value and acts as the final arbiter in accuracy disputes.
    Benefit: Defining the owner ensures information consumers work with correct data and producers write to the correct location. If not possible, it allows for analysis of potential conflicts and development of strategies to manage them.

Example: Motor Vehicle Registration System (Incomplete/Vague)

    Scenario: National system for motor vehicle registration with multiple semi-autonomous regional centers. Each center registers vehicles in its region and allocates a unique number.
    Problem: Potential conflicts in unique number allocation due to lack of real-time communication between regional centers. Each center is a "creator" of the vehicle registration number data item. (Clarification needed: How are these conflicts managed? What is the architectural strategy for uniqueness across regional centers without real-time communication?)

Information View

This section focuses on the Information View, detailing how information is owned, managed, and stored within the system.
Information Ownership and Partitioning

    Problem Resolution through Partitioning: Information ownership is resolved by allocating distinct ranges of numbers to each center for assigning to vehicles purchased in its area.
        Overlap Prevention: Ranges must never overlap. This is achieved by making each range significantly larger than the anticipated number of cars.
            Example Ranges:
                North center: 1 to 100 million
                West center: 101 million to 200 million
                (and so on for other centers)
    Interface Definition (By-product): Analysis of information ownership helps define high-level system interfaces.
        Interface Requirement: Where one system is an information owner and another is an information consumer (or maintains a copy), an interface is required.
        Cross-checking: Interface definitions derived from information ownership rules should align with process flows in the Functional View.

Enterprise-Owned Information

    Mandatory Usage: Large organizations often mandate the use of existing "enterprise" sources for important information rather than allowing systems to own and manage it independently.
    Value and Consequences: Enterprise information is highly valuable; incorrect or outdated enterprise data can have severe consequences for the system and the organization.
    Common Forms:
        Enterprise Reference Data: Most common form.
            Definition: Information on people, places, and things that categorizes or classifies transactional information.
            Types:
                General-purpose: country codes, currencies.
                Organization-specific: products, suppliers, customers.
        Volatile Enterprise Information: More frequently changing data, e.g., end-of-day stock levels, account balances.
    Access Models:
        Direct Access: System accesses information directly from the source system when needed.
        Local Copy: System maintains its own copy, refreshed regularly (real-time or batch).
        Updating Enterprise Information: System may need to update enterprise information itself, adhering to standard mechanisms and business processes defined by the information owner.
    Quality Requirements: Enterprise information used by the system must be accurate, up-to-date, consistent, and complete.
        Implications: Different achievement methods have implications for users and architecture.
    Example: Travel Agency Affinity Program:
        Scenario: Travel agency with branches, Internet sales, and call center wants a system to recommend holidays to customers based on preferences, budgets, and travel history.
        Enterprise Information Used:
            Holiday destinations, tour operators, airlines, hotels (reference information).
            Standard pricing plans, special offers (more volatile).
        Management Strategies for Enterprise Information:
            Holiday destinations, airlines, tour operators: Changes rarely; copy downloaded weekly to system's own database.
            Hotel information, list prices: More volatile; overnight extract required.
            Special offers: Short notice; "semi-real-time" feed (small batch extract at regular intervals during the day).
        Uploading Information: Affinity customers can suggest hotels not in the database; system needs to upload details to the enterprise store for validation and addition.
    Access Model Concerns:
        Batch Refresh: Data can be out-of-date when used.
        Real-time Access: Mitigates staleness but is more complex to implement and manage.
        Single Central Repository: Ensures up-to-date data but creates a bottleneck and single point of failure; may not be feasible for geographically dispersed systems.
        Further Discussion: These concerns are addressed in the Location perspective (Chapter 29).

Identifiers and Mappings

    Unique Identifiers: Each data item (relational entity or object/class) requires a unique identifier or key.
        Terminology:
            Relational databases: Primary key.
            Object-oriented programming: Object ID.
            General term: Identifier (does not assume underlying information model).
        Examples: Customer number, machine serial number, ISBN.
    Identifier Issues with Multiple Repositories:
        Discrepancies: Different systems may use different mechanisms to identify the same data item.
        Reconciliation: Mechanisms need to be reconciled at data exchange points.
        Volatility: Key assignment can be volatile (e.g., new orders per second in a sales system); reconciliation processes must be kept up-to-date.
    Example: Newspaper Sports Information:
        Scenario: Newspaper collates sports information from journalists and electronic sources, publishes daily league tables.
        Problem: Central database allocates identifiers, but most sources refer by name (often misspelled for foreign competitors).
        Information Quality Issues: Scores/results allocated to wrong player/team, "phantom" teams created, siblings' results misallocated, some results fail to load.
        Solutions:
            Architectural Capabilities: Defining standard identifiers.
            Business Process Changes: Users required to pick names from drop-down lists instead of typing (can make system awkward).
            Collaboration with Business Stakeholders: Find usable and effective solutions.
            Exception Workflow: Confirm correctness of automatically matched identifiers, allowing partial automation with manual input for data quality.
    Challenges with Identifiers:
        Invariance: Identifiers are normally invariant (never change over the data entity's lifetime).
        Exceptions: Not always possible to enforce invariance; mechanisms and business processes for creating/changing identifiers must be carefully specified.
        Subtleties of Identity: Deciding if two data entities represent the same thing and should share an identifier.
            Example: ISBNs for Book Editions:
                Problem: Second edition of a book (minor vs. substantial revisions) — new ISBN or same? If new, how linked to first edition? If same, how distinguished?
                Decision: Architect may need to decide or capture/agree on user requirements.
    User-Visible Identifiers: Consideration of whether identifiers will be seen by users.
        User-Visible: Debit/credit card numbers (used online/telephone).
        Not User-Visible: Individual purchase identifiers on a credit card statement (identified by date, merchant, amount for queries).

Volatility of Information Semantics

    Frequent and Unpredictable Change: Syntax, semantics, and interrelationships of business information often change.
        Examples: New fields, constraints, relationships, or entity types needed.
    Mitigation Strategies:
        Abstract database access libraries.
        Tools for impact analysis.
        Designing interfaces to allow for variation and change.
    Impact of Changes: Even small changes to an information model can have wide-ranging implications for consuming systems.
        Example: Adding a new mandatory field to a database table requires changes to all processes creating/updating rows in that table.
    Traditional Change Control:
        Process: Formal data model change control; impact on every system module assessed; database change rolled out only after all required functional changes are implemented.
        Pros: Established, effective.
        Cons: Drastically slows down system change rate; often subverted/bypassed.
    Alternative (Flexible) Approach: Decouple information semantics from physical storage structures.
        Method: Store complex information structures in structured text forms (XML, JSON, YAML) within a database or external files.
        Benefits: With discipline and automation, allows for more dynamic and flexible database schema changes (e.g., Evolutionary Database Design).
        XML Family Standards: Mature mechanisms for defining schemas and accessing content. Changes can often be implemented more quickly with less effort.
        XML Downsides: Less performant and scalable due to management overhead; most database optimizers do not work well with XML data.

Information Storage Models

    Dominance of Relational Databases: Third-normal-form relational databases are dominant in enterprise information systems.
    Four Major Types of Information Stores (in wide use):
        Relational Databases:
            Characteristics: Dominant landscape; typically third-normal-form schema; usually used as transactional or operational data store.
            Implementation: Typically uses third-party database management systems.
            Operations: Data retrieval and manipulation expressed declaratively using SQL.
            Integrity: Typically enforce data integrity via [INCOMPLETE SECTION: The text cuts off here, likely to describe how integrity is enforced. This needs clarification or completion by the downstream LLM.]

Information View

This section discusses various aspects of information within a system, including its storage, flow, consistency, and quality.

    Information Storage:
        Relational Databases (RDBMS):
            Use the ACID transaction model (Atomic, Consistent, Isolated, Durable).
            Avoid data duplication through normalization.
            Offer flexibility with unconstrained queries.
            Provide good performance and scalability for small to midsize problems.
            Limitations include scaling difficulties for very large problems and schema complexity in large enterprise applications.
        Dimensional Databases:
            Based on the relational model but use multidimensional ("star") schemas.
            Employ large "fact" tables linked to smaller "dimension" tables for classification.
            Well-suited for complex reporting but less so for transactional updates.
        NoSQL Databases:
            Trade off RDBMS characteristics (tabular storage, SQL queries, ACID) for simplicity, high scalability, and performance.
            Accessed via simple "map"-based interfaces for storing and retrieving records by key.
            Suitable for very large-scale Internet services but less so for rich, strongly typed databases or powerful query processing.
        File-Based Stores:
            Simple and ubiquitous; can offer good performance.
            Well-suited for "write-once" requirements like logging and auditing.
            Unsuitable for complex queries, reliable transactional updates, or complicated data structures.
        Architect's Role:
            Requires awareness of different information storage models.
            Needs to match the appropriate model to data storage requirements.

    Information Flow:
        Focuses on how information moves within the system.
        Key questions:
            Where is data created and destroyed?
            Where is data accessed, modified, and enriched?
            How do data items change as they move?
        Typically analyzed within Functional views but must ensure data-specific concerns are addressed.

    Information Consistency:
        Ensures compatibility and congruence of information across the system.
        Examples: Referential integrity constraints, matching summary data to underlying details.
        Transaction management is crucial (using techniques like two-phase commit or compensating transactions).
        Transaction Management:
            Ensures that a sequence of data updates occurs as an atomic unit.
            Modern relational databases provide transaction management features.
            Managing transactions across multiple systems is complex.
        Compensating Transactions:
            Each data update is committed individually.
            If a later update fails, committed updates are reversed.
        Eventual Consistency (BASE):
            Favors high availability over immediate consistency.
            Guarantees that all data instances will eventually be updated.
            Used in infrastructure software and some Internet-scale applications.

    Information Quality:
        The extent to which data values agree with real-world values.
        Poor quality can significantly impact operations.
        Considerations for architects:
            How will information quality be assessed and monitored?
            What are the minimum quality criteria?
            How will these criteria be enforced?
            How will poor-quality information be improved?
            Can good-quality information be corrupted?
            Can information quality degrade as it flows through the system?

Information View

This section focuses on the Information Viewpoint of the software architecture, detailing aspects related to information quality, timeliness, retention, and modeling.
Information Quality

    Addressing Poor-Quality Data:
        Development or deployment of automated tools for monitoring, assessing, or repairing poor-quality data may be necessary.
        If human intervention is required for data repair, a holding area for data awaiting manual repair may be needed.
        Workflow for Information Quality: Increasingly common to use workflow for information quality problems that cannot be easily automated.
            A central database manages a list of tasks (e.g., correcting customer name/address, dealing with suspect transactions).
            Tasks are assigned to users, and the system tracks their status to completion.
            Tasks can be standardized (defined at design time) or ad hoc (created at runtime).
            Service levels may be defined to commit to fixing problems within a certain time or rate.
            Well-designed workflow can effectively improve information quality and customer satisfaction.

Timeliness, Latency, and Age

    Context: Issues arise when information is not in a single data store or not accessed synchronously in real-time, leading to old or out-of-date information.
    Example (Commodity Brokerage):
        Scenario: A commodity brokerage accepts feeds (pricing, volume, news) channeled through a single gateway application. A catastrophic hardware failure renders the gateway unavailable for days. Upon recovery, subscribers are flooded with thousands of cached price messages that are days old and irrelevant.
        Solution: Gateway modified to discard cached price messages older than a configurable age after a failure. This significantly improved recovery in a subsequent failure.
    Key Concepts:
        Information Providers: External systems providing information (e.g., pricing, volume feeds).
        Information Consumers: Internal users utilizing information.
        Discrepancies: Can occur due to finite (and potentially long) time lag in information transfer. If time lag cannot be reduced, solutions for inconsistent information must be developed with stakeholders.
        Latency: Time lag between a data item being updated at the data source and the updated value being available to all parts of the system.
        Age of Data Items: Time since a data item was last updated by its data source. Systems dealing with volatile data (e.g., stock prices, truck locations) may not be interested in information that is hours or minutes old. This information may be discarded.
    Strategies to Handle Time-Based Inconsistencies:
        Tag important data items with a "last updated" date and time.
        Define "currency windows" for significant data items.
        Warn users when information may be outdated.
        Hide or discard information that may be too old.
        Reduce latency through faster interfaces or direct access to data sources.

Archiving and Information Retention

    Context: Information is rarely deleted due to legal reasons or historical analysis. While disk storage is inexpensive, managing large databases is complex, and physical storage cannot expand indefinitely.
    Need for Archiving: Sooner or later, information will grow to a point where it is not desirable to keep it all online, necessitating archiving to other storage media (e.g., high-capacity offline storage).
    Scope of Information to Archive:
        Must not include information needed for production activities.
        Should not include information likely to be useful for regular analysis.
        Usually selected based on age combined with business rules to determine usefulness.
    Impact of Archiving Strategy on Architecture:
        Archiving large volumes of information may make some systems fully or partly unavailable for significant periods.
        Physical disk sizing must account for information retention length.
        Processes for moving production information to archive media may need definition.
        Special actions may be required to ensure integrity and consistency of production and archive storage.
        Potential impact on network infrastructure if archive storage is remote.
    Architectural Consideration: Archival capabilities should be designed from the beginning as a natural part of the information lifecycle, not as an afterthought.

Stakeholder Concerns

    Acquirers: Concerned with preserving and safeguarding the value of information assets, including:
        Information quality and archiving
        Reference data
        Information retention
    Assessors: Interested in all aspects, with a focus on information structure and flow, identifiers and mappings, and information quality.
    Communicators: Rarely focus on detailed information architecture but may find a background understanding of key principles and strategies helpful.
    Developers and Maintainers: Interested in how architectural models translate into real databases and information interfaces (real-time, batch), implementation details (data structure support for processing, consistency guarantees).
    System Administrators: Interested in how real-world system components will be managed and supported.
    Testers: Interested in main database structures, how they are affected by system operation, data flow, and creation of realistic test data sets.
    Users: Concerned with functional aspects of information architecture (e.g., information ownership, regulation) and user-visible qualities such as timeliness, latency, age, and information quality.

Models

Data modeling is a well-established area for information systems. The three most important types of models are:

    Static information structure models
    Information flow models
    Information lifecycle models

Other useful models include information ownership models, information quality analyses, metadata models, and volumetrics models.
Static Information Structure Models

    Purpose: Analyze the static structure of information: important data elements and their relationships.
    Entity-Relationship (ER) Modeling:
        Established data analysis technique based on a solid mathematical model.
        Entities: Data items of interest.
        Attributes: Constituent parts of entities.
        Information Semantics: Defines static relationships among entities.
        Cardinality: Defines how many instances of one entity can be related to an instance of another.
        Example (Library):
            Entities: Books, Members, Authors, Publishers.
            Attributes: Book title, author name, ISBN number, publisher name and address.
        Notation: Figure 18-2 shows an ER diagram in crow’s foot style for the library example.
    Class Models:
        Similar role to ER models but for the object-oriented world.
        Model data items (classes), their constituent data parts (attributes), and static relationships (associations).
        Can be used to model relational entities by omitting behavioral aspects and limiting association types (e.g., no generalization or composition).
        Can document behavioral aspects (interfaces, methods) and object-oriented features (inheritance).
        Example (Library):
            Classes for books, members, authors, and publishers.
            Methods for checking out books.
        Notation: Figure 18-3 shows a UML class model for the library example.
    Star Schema (Multidimensional Schema or Cube):
        Specialized semantics used for data warehouses and data marts.
        Fact Tables: Contain numerical data or "facts" aggregated at many levels, with large compound keys.
        Dimension Tables: Clustered around each fact table, modeling different aggregation levels.
        Advantage: Aggregated values can be retrieved in a single database read.
        Snowflake Schema: Extends the star schema by normalizing dimension tables into a hierarchical structure.
        Example (Library): Figure 18-4 shows an example star schema for the library system (acknowledging that a library management system is unlikely to require a data warehouse in practice due to volume).

Activities in Formal Information Modeling

    The first step is to identify the important data entities.

Information View

This section details various models and concepts related to information within the software architecture.
Static Information Structure Models

These models focus on the static structure of information, typically derived by inspecting business processes and use cases.
Key Concepts

    Important Entities: Focus on a small number of crucial entities (e.g., customer, product, payment, event). Entities with "type" in their name can usually be ignored.
    Normalization: Reduces the model to its purest form, eliminating repeated, redundant, or duplicated information. Relational models rarely go beyond third-normal form. For architectural purposes, modeling some unnormalized information can be more useful.
    Domain Analysis: Examines attributes (fields) of data items and rules defining permissible values (e.g., customer number format, telephone number structure). This is usually too detailed for an architectural description (AD).
    Structural Decomposition and Aggregation:
        Structural Decomposition: Breaking an element into smaller, coherent pieces.
        Aggregation: Creating a new element by combining other, similar elements.
    Decomposition Limitations: Static information structure models (especially entity-relationship diagrams) are not easily decomposed into levels of detail. It is theoretically "all or nothing."
    Architectural Approach: Focus on a small number of the most important entities/classes and their relationships.
    Omissions:
        Intersection entities (replace with nonnormalized, many-to-many relationships).
        Type entities (e.g., product type).
    Guideline for Detail: If there are more than 20-30 entities, or if the entity-relationship diagram doesn't fit on a single page, there is likely too much detail. Consider removing less important entities or using partitioning/decomposition.

Information Flow Models

These models analyze the dynamic movement of information between system elements and the outside world.
Key Concepts

    Identification of Flows: Identify main architectural elements and the information flows between them. Each flow represents an information interface.
    Flow Attributes: Each flow has:
        Direction
        Scope of information transferred
        Volumetric information
        Means of exchange (in physical models)
    Example Scenario:
        Publisher supplies new book lists to libraries (PDF, monthly mail).
        Library receives books with electronic delivery notes (XML file, imported into book management system).
        Book checkout/return: new state recorded via bar-code readers.
        Book disposal: manually marked as deleted in the system by a PC application accessing the database directly.
        Note: Each italicized term in the example represents an information flow.
    Level of Detail: Aim for high-level and simple models; extensive detail is not necessary at the architectural stage. Most notations support natural decomposition.
    Applicability: Most useful for data-intensive systems. Complements interface and function invocation modeling in the Functional view, which is often for processing-intensive systems.
    Notation:
        Classic Systems Analysis: Gane and Sarson, SSADM data flow diagrams (these also cover process).
        UML: Activity diagrams (include similar elements).
        Figure 18-5 (Example Data Flow Diagram) Notation:
            Large rectangles: Processes that manipulate information.
            Narrow open rectangles: Data stores (logical or physical collections of information).
            Arrows: Information flows.
            Ellipses: External entities (people or other systems interacting with this system).
    Information Conveyed by Figure 18-5:
        Members and librarian provide information to checkout and return processes.
        Bookseller provides information to acquire book process.
        Librarian provides information to dispose of process.
        All this information is written to the BOOKS data store.
    Activities: Typically created through stepwise refinement, starting with important flows and detailing them as needed. Can be cross-checked against information ownership models for integrity in distributed ownership.

Information Lifecycle Models

These models analyze how information values change over time.
Entity Life Histories

    Concept: Model transitions data items undergo in response to external events, from creation through updates to deletion.
    Purpose: Useful for cross-checking that processing exists for all life events of an entity. Helps ensure controlled creation and deletion of entities.
    Example:
        Book created (published).
        Acquired by library.
        Repeatedly checked out and returned.
        Finally disposed of.
        Note: Each italicized verb represents an event in a book's entity life history.
    Notation: Usually represented by a tree structure with nodes for events and branches for iteration, selection (Figure 18-6).

State Transition Models (Statecharts in UML)

    Concept: Model overall changes in a system element's state in response to external stimuli. Useful for systems with complex, seemingly unpredictable state transitions.
    Finite State Machine (FSM): Models a system element as an FSM, which always has a current state (sum total of information it holds). External events cause deterministic state changes and potential processing.
    Example:
        Book is initially published.
        Then acquired by the library.
        Once on shelves, alternates between being available for loan and checked out.
        Until it is disposed of.
        Note: Each italicized term represents a state of a book.
    Notation: UML state diagram uses "railroad tracks" to represent possible state transitions (Figure 18-7).
    Activities: Derived from functional requirements by identifying significant events and their information impact.

Other Types of Information Models
Information Ownership Models

    Concept: Define the owner for each data item in the architecture. "Data item" typically means entity (table) or attribute (field), but more complex partitions can be modeled.
    Classes of Information Ownership:
        Owner or Master: Holds the definitive value.
        Creator: Creates new instances.
        Updater: Modifies existing instances.
        Deleter: Deletes existing instances.
        Reader: Can read but not change instances.
        Copy: Holds a read-only copy.
        Validater: Performs validation against business rules.
        A combination of these.
    Modeling: Can be modeled using a grid (Table 18-2), with systems and data stores on one axis and data items on the other. Each cell defines the ownership class.
    Trust and Permissions: Useful to develop a trust and permissions model to define which systems can modify data items under specific circumstances. This is important for system security.
    Conflict Resolution Strategies (when multiple creators/updaters/deleters exist):
        Always accept the latest update.
        Maintain multiple copies tagged with sources.
        Maintain a history of changes (not just latest version).
        Prioritize updates from a "trusted" system.
        Create complex rules based on data changed and nature of change.
        Record multiple values and require manual intervention for conflict resolution.
        Reject conflicting updates altogether.

Information View
Conflict Detection with Multiple Updaters

When multiple updaters are involved, detecting conflicts is crucial. This can be addressed by:

    Stamping each record with an incrementing version number.
    Recording the date and time the record was last updated.

Architectural design should provide sufficient guidance for designers on these strategies.
Information Quality Analysis

Architectural analysis of information quality focuses on identifying sources of poor-quality information and defining principles and strategies for handling it.

Possible strategies include:

    Accept poor-quality information: Suitable when the cost of repair outweighs the benefit, or when poor quality is not a significant issue.
        Example: An Internet search engine with millions of URLs. A small proportion become invalid, but regular cleanup is not cost-effective.
    Automatically fix poor-quality information: Utilizes tools specific to the information type.
        Example: Tools for repairing or completing addresses and phone numbers based on databases of postal codes or dialing rules.
    Discard poor-quality information: Best when the cost of bad information outweighs the cost of not having it.
        Example: A company discarding bulk mailing list records with missing or invalid postal codes to avoid penalties from the postal service and ensure mail delivery.
    Repair poor-quality information manually (user correction): A very costly approach requiring identification of poor-quality information and a process for forwarding it to users for correction.

Note: Be aware of potential legislative requirements for information quality (e.g., penalties for incorrect information on public members).
Metadata Models

Metadata is "data about data," consisting of rules that describe and prescribe data items (entities, attributes, relationships, etc.).

    Definition: ISO Standard 11197-3 defines metadata as "the information and documentation which makes data sets understandable and sharable for users."
    Aspects Addressed by Metadata:
        Data format (syntax)
        Data meaning (semantics)
        Data structure
        Data context (relationships among data items)
        Data quality
    Sources of Metadata Models:
        Enterprise-wide metadata models (if available).
        Cross-industry metadata models (e.g., Dublin Core Metadata Initiative).
    Relationship to Other Information Models: Closely allied with information structure models that include metadata elements (field attributes, relationships).
    Format: Most commonly structured or unstructured text, but more formal notations (e.g., XML-based) exist.
    Automated Tools: Some tools can extract metadata from large databases, particularly useful for legacy systems with poorly understood data internals.
    Industry Standard Data Models: May be useful for metadata analysis (e.g., ARTS Standard Relational Data Model for retail, ISO 20022 for financial services messaging).

Volumetric Models

Volumetric models analyze current and predicted data volumes.

    Scope: Can range from simple calculations to sophisticated statistical models or online simulations.
    Architectural Level: Usually kept fairly simple due to limited accuracy in early execution details.

Problems and Pitfalls
Representation Incompatibilities

Data incompatibilities arise from different systems representing field-level information in varying ways, either through different models or encoding schemes.

    Examples:
        Boolean values: Y/N vs. 1/0 vs. hex FF/00.
        Country codes: ISO abbreviations (FR, DE) vs. custom numeric encoding.
        Monetary amounts: Euros vs. local currency.
        Measurement units: Volume vs. weight.
        Financial values: Running totals vs. deltas.
    Resolution: These are usually easy to resolve.
    More Problematic: Incompatibilities between business models.
        Example: Integrating a telephone billing system (account-based, potentially joint accounts or no customer) with a sales system (customer-centric, needing account and payment history). The fundamental incompatibility requires complex processing.
        Solution Approach: Development of a subsystem or service responsible for maintaining links between customers and accounts, owned and managed by this service, and providing data on demand. Such a service would be core to the architecture with high performance, scalability, and availability targets.

Risk Reduction (Representation Incompatibilities)

    Develop a common, high-level model of data structure, key attributes, and domains, and validate it across all internal and external system parts.
    Review the model with business stakeholders to ensure it reflects reality.
    Focus on a small number of critically important attributes, rather than trying to model everything.
    Include external entities in the model (e.g., for data exchange with other organizations).
    Consider a data abstraction layer on top of data sources to hide incompatibilities from other architectural elements.

Unavoidable Multiple Updaters

Achieving a single update location for each data item is often not feasible in distributed architectures due to legacy systems, external information sources, or geographical/political limitations.

    Impact: Multiple creators/updaters can significantly impact the architecture and are not always easy to resolve.
    Architectural Awareness: Identify where this can happen to mitigate risks.

Risk Reduction (Unavoidable Multiple Updaters)

    Ensure the information ownership model is complete and accurate, identifying all data items with multiple updaters.
    Determine with stakeholders (primarily users) which multiple updaters are important and focus on those.
    Understand where inconsistencies can arise from multiple updaters and locate "crunch points" where incompatible data items meet.
    Develop resolution strategies (e.g., always overriding old updates with newer ones, maintaining two copies and manual resolution).

Key-Matching Deficiencies

Problems with key-matching often arise when integrating information from multiple systems. These may not become apparent until detailed design (expensive to change) or even during system operation.
Risk Reduction (Key-Matching Deficiencies)

    Identify keys for all entities and ensure compatibility across the architecture.
    At all points where information from different systems converges, ensure mechanisms for mapping keys from one system to another.
    Sample real data and perform consistency checks.
    Whenever possible, prioritize common keys and standardized information modeling.

Interface Complexity

The number of interfaces required between systems grows rapidly with the number of systems (n systems, n(n-1)/2 interfaces in the worst case). Changes to one system's interface impact many others, creating a significant development burden and barrier to change.
Risk Reduction (Interface Complexity)

    When interface requirements are complex, consider the integration hub architectural style.
    Integration Hub Model: All systems are linked once via a specialized adapter to a central integration hub.
        The adapter performs system-specific translation.
        The hub handles message routing, resilience, and specialized functions (publish/subscribe, acknowledgment, guaranteed delivery).
    Advantage: If a system changes, often only its adapter needs modification.

Information View

This section discusses common architectural patterns and risks related to information management within a software system.
Integration Hubs

    Concept: A central hub used to integrate multiple disparate systems.
    Mechanism: Adapters for each system connect to the hub. The hub handles routing, resilience, and other common integration concerns.
    Benefits:
        Reduces the number of direct integrations (N-squared problem).
        Centralizes specialized logic (routing, resilience).
    Disadvantages:
        Single point of failure.
        Potential scalability bottleneck.
        Can slow down change due to difficulty in scheduling/prioritizing changes to a critical shared component.
    Implementation: Typically implemented using third-party commercial or open-source integration hub products.
    Related Topics: Forms part of Enterprise Application Integration (EAI).

Overloaded Central Database

    Concept: Storing all system information in a single, central database.
    Benefits:
        Simpler design: no key mappings, update reconciliation, or complex interfaces.
        All data is immediately available.
    Disadvantages:
        Single point of failure.
        Potential performance bottleneck.
        Poor latency for remote users in geographically distributed systems.
        System availability constrained by global network limitations.
        Data model can become overloaded or unworkable.
        Can cause design-time and runtime contention.
    Risk Reduction Strategies:
        Carefully consider likely growth in data volumes, users, and locations (covered in Chapter 28: Evolution perspective).
        Consider designing for a separate reporting database from the main operational data store.
        Plan a data partitioning strategy for the future, even if not immediately implemented.
        If using a central database, ensure scalability options are available.
        Investigate database clustering and other availability/performance improvement mechanisms.

Inconsistent Distributed Databases

    Concept: Replicating information between multiple databases in different locations or geographical regions.
    Benefits:
        Brings data closer to users, reducing latency.
        Improves availability.
    Disadvantages:
        Harder to design and build.
        Often leads to information inconsistency due to replication delay.
        Updates are harder to manage when replicate copies are not read-only.
    Risk Reduction Strategies:
        Carefully balance the benefits of distribution against increased complexity and data inconsistency.
        Implement effective strategies for dealing with inconsistency, agreed upon with stakeholders (especially users).
        Ensure effective operational tools and processes for detecting and handling problems that cannot be dealt with automatically.

Poor Information Quality

    Problem: Inconsistent, inaccurate, or incomplete data. Unexpectedly poor quality is the main issue.
    Impact: Leads to significant operational problems.
    Risk Reduction Strategies:
        Validate key assumptions about information quality early (e.g., unique global product identification).
        Understand and prioritize important vs. less important information (with stakeholder input).
        Utilize commercial information quality tools for existing data analysis.
        Identify sources of poor-quality information and develop strategies (rejecting, marking as suspect, attempting to fix).

Excessive Information Latency

    Problem: Arises from overly complex architectures, systems not designed for expected information volumes, or external constraints (e.g., weekly external data feeds, overnight batch updates).
    Impact: Becomes an issue if latency is unexpectedly poor.
    Risk Reduction Strategies:
        Predict information latency between providers and consumers, especially with distance or complexity.
        Review significant latency with stakeholders to determine if it is a concern.
        Obtain agreement on realistic latency requirements for all data items upfront and validate the model against these.

Inadequate Volumetrics

    Problem: Not clearly defining the expected volumes of information the system must handle.
    Impact: Leads to an inappropriate architecture unable to cope with actual loads. (More detail in Chapter 26).
    Risk Reduction Strategies:
        Capture, review, and approve data volumes with stakeholders.
            Separate "business" volumes (e.g., numbers of orders) from "technical" volumes (e.g., database updates).
        Ensure volumes are realistic; increase if doubt exists.
        Cover all scenarios: online day, overnight processing, peak periods (year-end, holidays).
        Ensure effective translation of business volumes into physical ones (e.g., one business transaction leads to multiple physical transactions).
        Account for future expansion in volume estimates.
        Prototype data stores and access for expected volumes.

Checklist for Information View

    Data Models:
        Appropriate level of detail (e.g., 20-30 entities).
        Support current and future processing requirements.
        Clearly identified keys for important entities.
        Defined mappings between keys for entities distributed across multiple systems/locations.
        Processes for maintaining key mappings when data items are created.
        Accounted for derived data (e.g., account balances from activity).
        Defined strategies for resolving data ownership conflicts (multiple creators/updaters).
    Latency & Consistency:
        Clearly identified latency requirements and mechanisms to achieve them.
        Clear strategies for transactional consistency across distributed data stores, balancing need with performance/complexity costs.
    Data Storage:
        Considered various data storage models, their strengths, and weaknesses.
        Mechanisms for validating migrated data and dealing with errors.
        Right types of data stores (operational, reporting, data warehouses, data marts) for volumes and performance.
        Sufficient storage and processing capacity for archiving and restoring archived data.
    Data Quality:
        Data quality assessment performed.
        Strategies created for dealing with poor-quality data.
    Enterprise Sources:
        Confirmed which entities should be obtained from shared enterprise sources.
        Architecture appropriately utilizes shared enterprise sources.

Further Reading (References provided in original document)

    Information Architecture (general): Sparse literature.
    Data Modeling (Relational):
        Date [DATE03]
        Elmasri and Navathe [ELMA99]
        Kroenke [KROE02]
    Newer Database Techniques (Object-Oriented):
        Kim [KIMW99]
    Data Quality:
        Redman [REDM97]
    Enterprise Application Integration (EAI):
        Linthicum [LINT03]
        Ruh et al. [RUHW00]
    Metadata Modeling:
        ISO Standard 11197-3 [ISO96]
        Marc [MARC00] (Possibly [MARC00] refers to a book by Marc, but no specific title is given in the text)
        Specific metadata models: ARTS Standard Relational Data Model, ISO 20022 (financial services messaging)
    Database Refactoring / Evolutionary Database Design:
        Ambler and Sadalage [AMBL06]
    Data Warehousing:
        William Inmon [INMO05]
        Ralph Kimball [KIMB02]
        Alec Sharp and Patrick McDermott [SHAR08]
    Specific Relational Database Products & Tools: Numerous books available (Oracle, SQL Server, DB2, Sybase, MySQL).
    Non-relational Database Technologies (NoSQL): Internet is the best source of information.