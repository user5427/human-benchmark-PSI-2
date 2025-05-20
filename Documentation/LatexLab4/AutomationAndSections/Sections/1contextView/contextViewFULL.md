Context Viewpoint
Definition

    Describes relationships, dependencies, and interactions between the system and its environment (people, systems, external entities).

    Defines system scope, boundaries, and external interactions while representing the system as a "black box."

Concerns

    System Scope and Responsibilities

        High-level list of key capabilities or requirements.

        Explicit exclusions (optional but recommended).

        Example for an online retailer:

            Capabilities:

                Present catalog with product details.

                Flexible search functionality.

                Accept orders and credit card payments.

                Automated back-end fulfillment interfaces.

            Exclusions:

                Order amendments/cancellations (manual process initially).

                Non-credit-card payment methods.

                Live stock level display/reservations.

    Identity of External Entities and Services/Data Used

        External entities include:

            Internal systems (same organization).

            External systems (other organizations).

            Gateways hiding other systems.

            External data stores (shared databases).

            Peripheral devices (messaging appliances).

            Users/roles (operational staff, support).

        Entities must interact via services/data (otherwise irrelevant).

    Nature and Characteristics of External Entities

        Quality properties affecting architecture:

            Stability, availability, performance, location, data quality.

        Example: Travel booking system with intermittent external systems requires:

            Configurable retry logic.

            Idempotent interactions.

            Partial transfer resumption.

        Focus on externally visible properties only.

    Identity and Responsibilities of External Interfaces

        Interface purposes:

            Data provider/consumer: Direct data transfer.

            Service provider/consumer: Action requests/responses.

            Event provider/consumer: Event notifications.

        For each:

            Data: Content, scope, meaning.

            Services: Semantics, parameters, error handling.

            Events: Meaning, content, volume, timing.

    Nature and Characteristics of External Interfaces

        Quality properties (may differ from connected systems):

            Bandwidth, reliability, latency.

        Characteristics:

            Volumes (requests/data size/growth).

            Interaction triggers (scheduled/event-driven/ad hoc).

            Automation level (manual/semi-automated/fully automated).

            Transactionality (all-or-nothing completion).

            Criticality/timeliness (e.g., end-of-day deadlines).

Models

    Context Model: High-level diagram of system and external entities.

    Interaction Scenarios: Descriptions/key flows (e.g., payment authorization).

Problems/Pitfalls

    Missing/incorrect external entities.

    Implicit dependencies not documented.

    Vague interface descriptions.

    Scope creep or inappropriate detail level.

    Overcomplicated interactions or jargon.

Stakeholders

    Primary: Acquirers, users, developers.

    Secondary: All stakeholders (for alignment).

Applicability

    All systems (mandatory for clarity).

Incomplete/Unclear Areas

    No specific examples for "Nature of External Interfaces" (e.g., protocol details).

    Missing concrete metrics for interface characteristics (e.g., "low bandwidth" not quantified).

Diagram Hints

    Context model likely uses boxes/lines notation:

        Central "black box" for the system.

        Labeled boxes for external entities.

        Arrows indicating interaction directions/types (data/service/event).

    Interaction scenarios may use sequence diagrams or flowcharts.
Context Viewpoint (Continued)
Other External Interdependencies

    Types of dependencies:

        Non-functional/data-flow dependencies (e.g., hidden data replication).

        Directional: System → External entity or vice versa.

    Example: Online retailer’s e-Commerce System depends on:

        Payment System: Collects payments.

        Customer Accounts System: Updates shipping addresses.

        Fulfillment System: Dispatches goods (rejects orders if address not in its replicated database).

        Architectural impact:

            Resubmit failed orders after replication delay.

            Delay orders with address updates to allow replication.

            Interface design should expose failure reasons (e.g., "address not found").

Impact of the System on Its Environment

    Key considerations:

        Dependent systems: May require functional/interface changes or performance upgrades.

        Decommissioned systems: List systems to be retired post-deployment.

        Data migration: Identify datasets to be migrated into the new system.

    Note: Track these changes even if owned by other teams (e.g., enterprise architects).

Overall Completeness, Consistency, and Coherence

    Goal: Ensure end-to-end solution aligns with user needs across the application landscape.

    Coverage checks:

        Business processes must have full coverage (systems or manual processes).

        Data must be accessible to all systems needing it.

    Example: Early e-commerce systems focused on UI but neglected payment/fulfillment, leading to reputational damage.

Stakeholder Concerns (Detailed Table)
Stakeholder Class	Concerns
Acquirers	System scope/responsibilities, external entities/services/data, system impact on environment.
Assessors	All concerns.
Communicators	System scope/responsibilities, external entities/interfaces.
Developers	All concerns.
Production Engineers	External interface characteristics, system impact on environment.
System Administrators	All concerns.
Testers	All concerns.
Users	System scope/responsibilities, external entities/services/data, overall solution coherence.
Models

    Context Model:

        Purpose: Show system as a black box interacting with external entities.

        Elements:

            System: No internal details.

            External Entities: Labeled with name, type (system/data store/person), owner, responsibilities.

            Interfaces: Summarized data/function flows; may roll up multiple interfaces into one.

        Audience: Broad (business + technical stakeholders).

        Design Principles:

            Simple, jargon-free, business-language terms.

            Single-page diagram preferred.

        Notation Options:

            UML Workaround:

                System as a <<subsystem>> stereotyped component.

                Human-facing external entities as UML actors.

                Non-human entities as components/classes with dependencies.

            Boxes-and-Lines: Informal but widely understood.

    Interaction Scenarios (Implied but not detailed in this section):

        Example: Payment authorization → fulfillment workflow.

Incomplete/Unclear Areas

    Interface Protocols/Formats: Mentioned (e.g., "open standards or proprietary") but no examples.

    Security Levels: Listed (authentication, confidentiality) but not elaborated.

    UML Diagram Rules: No explicit guidance on representing non-human external entities (e.g., data stores).

Diagram Hints (Context Model)

    Layout: Central system box with surrounding external entities.

    Connections: Arrows labeled with interaction types (e.g., "Order Data," "Payment Request").

    Annotations: Brief interface semantics (e.g., "Batch transfer nightly").

    Example Reference: Figure 16–1 (e-Commerce System dependencies).

Context Viewpoint (Final Section)
Notation Details (Context Model)

    UML Representation:

        System: Represented as a <<subsystem>> stereotyped component.

        External Entities:

            Human-facing: UML actors (optionally with customized stereotypes/icons).

            Systems: Additional subsystem components or actors.

        Interfaces:

            UML information flows, dependencies, or associations.

            Optional: "Conveyed information" icons (small black arrowheads on associations).

        Limitation: UML provides weak native support for context modeling.

    Boxes-and-Lines Notation:

        Informal, ad hoc diagrams (e.g., Figure 16–3).

        Advantages:

            More expressive than UML.

            Easier for non-technical stakeholders.

        Disadvantages:

            Requires custom notation definition.

            Potential disconnect from UML-based architectural models.

Activities for Context Definition

    Documentation Control:

        Maintain a single, version-controlled document.

        Restrict access if sensitive (e.g., system decommissioning plans).

    Steps to Prepare Context Model:

        Review System Goals:

            Capture business/technology objectives (e.g., "Reduce cost per transaction by 15%").

        Summarize Key Functional Requirements:

            Group by subject area (align with scope definition).

        Identify External Entities:

            Systems, gateways, data stores, devices, users/roles.

            Over-include initially; refine later.

        Define Entity Responsibilities:

            Map services/data provided/consumed.

        Identify Interfaces:

            Data flows and service invocations (bidirectional).

        Validate Interface Definitions:

            Ensure compatibility with usage.

            Reference external documentation if needed.

        Walk Through Requirements/Scenarios:

            Validate flows and add missing interfaces.

Interaction Scenarios

    Purpose:

        Uncover implicit requirements (ordering, volume, timing constraints).

        Clarify contentious or poorly understood interactions.

    Notation:

        Textual interaction lists (like use case definitions).

        UML sequence diagrams (preferred for graphical representation).

    Example: Complex payment authorization → fulfillment workflows.

Problems and Pitfalls

    Missing/Incorrect External Entities:

        Risk: Late-stage project changes or delivered system incompleteness.

        Mitigation:

            Engage diverse stakeholders for validation.

            Involve domain experts early.

            Implement change management post-stabilization.

    Missing Implicit Dependencies:

        Example: Data replication latency between external systems.

        Mitigation:

            Document all assumptions and validate with stakeholders.

    Loose Interface Descriptions:

        Risk: Architectural impacts misunderstood.

        Mitigation:

            Capture sufficient detail for architectural decisions.

            Avoid glossing over complexity.

    Inappropriate Detail Level:

        Guidelines:

            Context diagram fits on one page.

            Scope definition: 2–3 pages max.

            Group requirements by functional area.

            Limit external entities to 10–20 (group if exceeded).

    Scope Creep:

        Causes:

            Uncontrolled "nice-to-have" additions.

            Poor change management.

        Mitigation:

            Challenge scope changes rigorously.

            Educate stakeholders on trade-offs (time, cost, feasibility).

Incomplete/Unclear Areas

    Interaction Scenario Examples: No concrete instances provided (e.g., sample sequence diagrams).

    Change Management Process: Mentioned but not detailed (e.g., approval workflows).

    Tooling: References to UML tools but no specific recommendations.

Key Diagram Hints

    Boxes-and-Lines Notation:

        Use color/shapes to distinguish entity types (e.g., users vs. systems).

        Label interfaces with interaction types (e.g., "Batch Data Transfer").

    UML Sequence Diagrams:

        Lifelines for system and external entities.

        Focus on critical message exchanges (e.g., error responses).

    Context Viewpoint
Key Responsibilities

    Define the system's scope and interactions with external entities.

    Identify all external entities the system interacts with and their responsibilities.

    Document the nature of each interface with external entities.

    Consider dependencies between external entities.

    Ensure stakeholders agree on the context model.

Risk Reduction Guidelines

    Scope Changes

        Ensure scope changes are managed once stabilized.

        Avoid uncontrolled scope creep impacting cost, complexity, or stability.

    Overcomplicated Interactions

        Some external systems (especially older ones) may have:

            Unusual data encodings.

            Poorly understood conversational protocols.

            Complex proprietary interface technologies.

        Mitigations:

            Understand interfaces early in design.

            Prototype and test interactions thoroughly.

            Secure expertise for unfamiliar interfaces.

    Overuse of Jargon

        Avoid terminology not widely understood.

        Provide a glossary if jargon is necessary.

Checklist for Context Model Validation

    Stakeholder Alignment

        Consulted all relevant stakeholders.

        Obtained formal agreement on the context model.

        Ensured other teams understand implications.

    External Entities & Interfaces

        Identified all external entities and their responsibilities.

        Documented each interface’s nature and detail level.

        Considered dependencies between external entities.

        Validated coherence of the overall solution.

    Scope Definition

        Internally consistent and at appropriate detail.

        Includes key capabilities/requirements.

        Specifies technology constraints (e.g., mandated platforms).

        Explicitly states "obvious" assumptions.

    Operational Governance

        Context model under formal change control.

        Change process followed with stakeholder consultation.

        Model stored in an accessible location (e.g., shared folder/wiki).

    Scenario Validation

        Explored realistic interaction scenarios.

        Verified coverage of main business processes.

        Confirmed data availability (on-site/external) for processes.

Incomplete/Vague Areas

    No specific examples of "unusual data encodings" or "proprietary technologies."

    Glossary of jargon not provided in this excerpt.

    Change control process details (e.g., approval workflow) unspecified.

Diagram Hints

    Context diagram should:

        Illustrate all system-environment interfaces.

        Include clear definitions supporting the diagram.

Further Reading References

    Garland and Anthony ([GARL03]): Context viewpoint.

    Bosch ([BOSC00]): System context definition.

    Sommerville and Sawyer ([SOMM97]): Requirements scoping.

    Tuckman ([TUCK65]): Group development model.

Notes for Downstream Processing

    Preserved original terminology (e.g., "conversational protocols," "change-managed").

    Grouped checklist items by theme for structured analysis.

    Flagged areas needing clarification (e.g., interface examples, glossary).

    Included citations for further reading.