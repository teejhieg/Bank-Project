import React from "react";
import { Link, useParams, useNavigate } from "react-router-dom";

class AddAccount extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            accountData: {
                firstName: "",
                lastName: "",
                phoneNumber: "",
                dateOfBirth: new Date().toString(),
            }
        };

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        switch (event.target.id) {
            case "firstName":
                this.setState(prevState => ({ accountData: { ...prevState.accountData, firstName: event.target.value } }));
                break;
            case "lastName":
                this.setState(prevState => ({ accountData: { ...prevState.accountData, lastName: event.target.value } }));
                break;
            case "phoneNumber":
                this.setState(prevState => ({ accountData: { ...prevState.accountData, phoneNumber: event.target.value } }));
                break;
            case "dateOfBirth":
                this.setState(prevState => ({ accountData: { ...prevState.accountData, dateOfBirth: event.target.value } }));
                break;
            default:
                break;
        }
        console.log(this.state.accountData);
    }

    render() {
        function SubmitButton(props) {
            const navigate = useNavigate();

            const handleClick = () => {
                fetch('http://localhost:5138/api/Accounts', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        firstName: props.accountData.firstName,
                        lastName: props.accountData.lastName,
                        phoneNumber: props.accountData.phoneNumber,
                        dateOfBirth: props.accountData.dateOfBirth,
                    })
                }).then(response => {
                    if (response.status === 201) {
                        navigate("/");
                    }
                    else {
                        alert("An error occurred during save. Please try again.");
                    }
                });
            }

            return(
                <button type="button" className="btn btn-outline-primary" onClick={handleClick}>Submit</button>
            )
        }

        return (
            <div className="container">
                <h1 style={{ textAlign: "center" }}>Create New Account</h1>
                <div>
                    <form>
                        <div className="row justify-content-center">
                            <div className="mb-6 col-6">
                                <label className="form-label">First Name</label>
                                <input type="text" className="form-control" id="firstName" value={this.state.accountData.firstName} onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="row justify-content-center">
                            <div className="mb-3 col-6">
                                <label className="form-label">Last Name</label>
                                <input type="text" className="form-control" id="lastName" value={this.state.accountData.lastName} onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="row justify-content-center">
                            <div className="mb-3 col-6">
                                <label className="form-label">Phone Number</label>
                                <input type="text" className="form-control" id="phoneNumber" value={this.state.accountData.phoneNumber} onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="row justify-content-center">
                            <div className="mb-3 col-6">
                                <label className="form-label">Date of Birth</label>
                                <input type="date" className="form-control" id="dateOfBirth" value={this.state.accountData.dateOfBirth} onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="row justify-content-center">
                            <div className="mb-3 col-3">
                                <SubmitButton accountData={this.state.accountData}/>
                            </div>
                            <div className="mb-3 col-1">
                                <Link className="btn btn-outline-danger" to="/">Cancel</Link>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        );
    };
}

export default AddAccount; 