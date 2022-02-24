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
                <li key={account.id}>{account.firstName}</li>
            );

            return (
                <ul>{listAccounts}</ul>
            );
        }

        return (
            <div className="d-grid gap-2 col-6 mx-auto">
                <h1></h1>
                <AccountList accounts={this.state.accounts}/>
                <Link className="btn btn-outline-primary btn-lg" to="/addaccount">Add Account</Link>
            </div>
        );
    };
}

export default Dashboard; 