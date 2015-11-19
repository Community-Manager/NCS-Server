# Neighbors-Community-Manager
A community management system for easier governance and administration the common residential areas.

Server endpoints
----------------

**Each endpoint signature can be changed during development, feel free to make new additions or corrections where you think it is necessary.**
------------------------------------------------------------------------

**TIPS:** 
Add **"READY"** prefix in front of each point that is fully implemented.
Add **"WORKING"** prefix in front of each point that is currently under construction.


**I. UserController** 

 1. READY! - Register User **(POST)** - **api/account/register** -> RequestBody -> {email, password, confirmPassword, firstName, lastName, appartmentNumber, verificationToken}
 2. Update User by ID - **(PUT)** - **api/user/{id}**
 3. Delete User by ID - **(DELETE)** - **api/user/{id}**
 4. Delete User by Email - **(DELETE) - api/user/{email}**
 5. Get User by ID - **(GET) - api/user/{id}**
 6. Get User by Apartment Number - **(GET) - api/user/{apartment-number}**
 6. Get all Users - **(GET) - api/user**
 7. Get all Users without the current logged user - **(GET) - api/users-available**
 8. Get all Users matching a given Name - **(GET) - api/user/{name}**
 9. Get all Users that have taxes which are not paid - **(GET) api/user/taxes-not-paid**
 10. Get all Users that have concrete tax which is not paid **(GET) api/user/taxes-not-paid/{tax-id}**
(Example: Tax - "Elevator electricity and support" -> Ivan, Gosho, Marto, Mariyan have not paid)

**II. TaxController - READY**
*Authorization: Administrator, Accountant*

 1. Create Tax - **(POST) - api/taxes**
 2. Get Tax by Id - **(GET) - api/taxes/get/{id}** 
 3. Update Tax by ID - **(PUT) - api/taxes/update/{id}**
 4. Delete Tax by ID - **(DELETE) - api/taxes/delete/{id}** *marks as deleted*
 5. Remove Tax by ID - **(DELETE) - api/taxes/remove/{id}** *removes entity from DB*
 6. Get all Taxes - **(GET) - api/taxes** *Removed- users should not be able to get all taxes in the system*
 7. Get all Taxes by CommunityId - **(GET) - api/taxes/community/{id}**
 8. Get all Taxes which are available for payment by community ID - **(GET) - api/taxes/available/{id}**
 9. Get all Taxes which are not-available for payment (their deadline has passed) for current community - **(GET) - api/taxes/expired/{id}**

**III. ProposalController**

 1. Create Proposal - **(POST) - api/proposal**
 2. Update Proposal by ID - **(PUT) - api/proposal/{id}**
 3. Delete Proposal by ID - **(DELETE) - api/proposal/{id}**
 4. Delete all Proposals by given AuthorID - **(DELETE) - api/proposal/{author-id}**
 5. Get Proposal by ID - **(GET) - api/proposal/{id}**
 6. Get all Proposals - **(GET) -api/proposal**
 7. Get all Proposals by given AuthorID - **(GET) - api/proposal/{author-id}**
 8. Get all Proposals which are Approved - **(GET) - api/proposal/approved**
 9. Get all Proposals which are Pending approval - **(GET) - api/proposal/pending**

**IV. InvitationController**
 1. READY! - Send Invitation - **(POST) - api/invitation** -> RequestBody { email, communityName }
 2. READY! - Get All Invitations - **(GET) - api/invitation**

**V. CommunitiesController**
 1. READY! - Get all Communities - **(GET) - api/communities** 
 2. READY! - Create new Community with appended administrator - **(POST) - api/communities** -> RequestBody {check **CommunityWithAdminDataTransferModel.cs**}
