# DDD No Duplicates Problem

This repository offers some ways to tackle the common problem in DDD where a business rule (invariant) of the domain model requires data from outside the model. This often results in either domain logic leaking into the service layer or external dependencies being injected into the domain class. A classic example of this is when trying to enforce uniqueness when creating/updating a user name, user email, product name, etc.

For more details on the problem [this](https://enterprisecraftsmanship.com/posts/domain-model-purity-completeness/) is a good article.

There are more complex solutions to this problem, e.g. involving passing events between an aggregate and its children (see [here](https://github.com/ardalis/DDD-NoDuplicates) for some examples). However, I find these solutions overly complex and find some leakage of domain logic into the service layer an acceptable trade-off for simplicity as long as the domain class is still fully encapsulated, e.g. even if the checking of the rule is carried out in the service layer the outcome is still a required by the domain model to perform the operation.

Another approach is to take a performance hit by loading the full dataset and passing it to the domain object to apply its business rule, such is the case of moving the aggregate root down a level so that it contains the entire collection of items and performing add/update operations directly on the aggregate. This may be an acceptable alternative in cases that don't need to scale.
