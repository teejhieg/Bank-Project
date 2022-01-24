import logo from './logo.svg';
import './App.css';
import {
  BrowserRouter as Router,
  Routes,
  Route,
} from "react-router-dom";

import AddAccount from './components/AddAccount';
import Dashboard from './components/Dashboard';

function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route exact path="/" component={Dashboard} />

          <Route exact path="/addaccount" component={AddAccount} /> 

          <Route path="*" component={Dashboard} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
