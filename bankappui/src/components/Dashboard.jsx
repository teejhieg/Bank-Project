import React from "react";
import { Link } from "react-router-dom";

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isolated: false,
            accounts: []
        };
    }

    componentDidMount() {
        fetch("http://localhost:5138/api/Accounts")
            .then(response => response.json())
            .then(
                (result) => {
                    this.setState({
                        accounts: result
                    });
                }
            )
    }

    render() {
        function AccountList(props) {
            const listAccounts = props.accounts.map((account) =>
                <div className="shadow-lg mb-5 bg-body rounded">
                    <div className="card-title" key={account.id}>{account.firstName}</div>
                    <div className="card">
                        <ul className="list-group list-group-flush">
                            <li className="list-group-item">Phone Number</li>
                            <li className="list-group-item">Date of Birth</li>
                            <li className="list-group-item">Account Balance</li>
                            <li className="list-group-item">Account ID</li>
                            <button type="button" class="btn btn-link">Account Details</button>
                        </ul>
                    </div>
                </div>
            );

            return (
                <ul>{listAccounts}</ul>
            );
        }

        return (
            <div className="d-grid gap-2 col-6 mx-auto">
                <h1></h1>
                <AccountList accounts={this.state.accounts} />
                <Link className="btn btn-outline-primary btn-lg" to="/addaccount">Add Account</Link>
            </div>
        );
    };
}

export default Dashboard; 