using System.Collections.Generic;
using System.Linq;

namespace BlalApi.Authentication
{
    public static class CustomError
    {
        private static List<Error> Errors
        {
            get
            {
                return GetErrors();
            }
        }

        public static Error GetError(string errorCode)
        {
            return Errors.FirstOrDefault(e => e.Code == errorCode);
        }

        private static List<Error> GetErrors()
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error { Code = Constants.EC_InternalServerError, Description = "Internal server error." });
            errors.Add(new Error { Code = Constants.EC_ObjectReferenceNull, Description = "Some of the object value found null that must not be null." });
            errors.Add(new Error { Code = Constants.EC_DeviceIDRequired, Description = "Device Id required in the request header." });
            errors.Add(new Error { Code = Constants.EC_InvalidApiKey, Description = "Invalid api key." });
            errors.Add(new Error { Code = Constants.EC_InvalidUserCredentials, Description = "Invalid username/password." });
            errors.Add(new Error { Code = Constants.EC_UserNotRegistered, Description = "Username is not registered in the system." });
            errors.Add(new Error { Code = Constants.EC_EmailNotFoundInRequestUser, Description = "Email Id is not found in given user." });
            errors.Add(new Error { Code = Constants.EC_EmailNotRegistered, Description = "Email is not registered in the system." });
            errors.Add(new Error { Code = Constants.EC_InvalidUserId, Description = "User not found/Invalid User id." });
            errors.Add(new Error { Code = Constants.EC_OldPasswordWrong, Description = "Old password doesn't match." });
            errors.Add(new Error { Code = Constants.EC_ValidationFailed, Description = "Required data not found/request data failed to validate." });
            errors.Add(new Error { Code = Constants.EC_UserWithoutDonor, Description = "User({0}) does not have donor role." });
            errors.Add(new Error { Code = Constants.EC_BranchNotFound, Description = "Branch not found/invalid branch id." });
            errors.Add(new Error { Code = Constants.EC_DonorNotFound, Description = "Donor not found/invalid donor id." });
            errors.Add(new Error { Code = Constants.EC_WrongCharityOfDonor, Description = "Charity id is not related to this person." });
            errors.Add(new Error { Code = Constants.EC_CharityNotFound, Description = "Charity not found/invalid charity id." });
            errors.Add(new Error { Code = Constants.EC_StripeCharityNotAssociate, Description = "Charity isn't associate with any stripe account." });
            errors.Add(new Error { Code = Constants.EC_StripeUserNotAssociate, Description = "User isn't registered with stripe." });
            errors.Add(new Error { Code = Constants.EC_StripeNoCardDetail, Description = "Card details is not found." });
            errors.Add(new Error { Code = Constants.EC_StripePaymentUnsuccess, Description = "Stripe - stripe payment could not get succeed, error - {0}" });
            errors.Add(new Error { Code = Constants.EC_StripeApiError, Description = "Stripe - received from stripe api - {0}" });
            errors.Add(new Error { Code = Constants.EC_UserDonorNotBelong, Description = "User and donor not related to each other." });
            errors.Add(new Error { Code = Constants.EC_InvalidBranchWithCharity, Description = "Branch Id isn't exist in the same charity which is assigned to this donor." });
            errors.Add(new Error { Code = Constants.EC_DonationNotFoundWithFilters, Description = "Donation not found in given searching criteria." });
            errors.Add(new Error { Code = Constants.EC_BranchNotFoundWithCharity, Description = "Branches not found with given charity id." });
            errors.Add(new Error { Code = Constants.EC_LicenceExpired, Description = "Your organisation's license has been expired, please contact to administrator." });
            errors.Add(new Error { Code = Constants.EC_BrainTreePaymentFailed, Description = "Braintree payment failed." });
            errors.Add(new Error { Code = Constants.EC_BrainTreeGateWayFailed, Description = "Braintree gateway failed." });
            errors.Add(new Error { Code = Constants.EC_CharityBrainTreeNotFound, Description = "Charity braintree details not found." });
            errors.Add(new Error { Code = Constants.EC_BrainTreeCustomerFailed, Description = "Braintree payment failed." });
            
            return errors;
        }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
}