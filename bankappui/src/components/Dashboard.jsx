import React from "react";
import { Link } from "react-router-dom";

const Dashboard = () => {
    return (
        <div class="d-grid gap-2 col-6 mx-auto"> 
            <h1></h1>
            <Link className="btn btn-outline-primary btn-lg" to="/addaccount">Add Account</Link>
        </div>
    );
};

export default Dashboard; 