import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);
    this.loggedIn=document.cookie.includes("token=");
    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }
  
  toggleNavbar () {
    this.loggedIn=document.cookie.includes("token=");
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  logoutClick= (e)=> {
    document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    this.props.history.push("/log-in");
  }

  render = () => {
    return (
        <div>
            <ul id="dropdown" className="dropdown-content">
                {!this.loggedIn ? <li><a href={this.props.basename+"/log-in"}>Log In</a></li>:null}
                {!this.loggedIn ? <li><a href={this.props.basename+"/sign-up"}>Sign Up</a></li>:null}
                {this.loggedIn ? <li><a href={this.props.basename+"/log-in"} onClick={this.logoutClick}>Log Out</a></li> : null}
                {this.loggedIn ?<li><a href={this.props.basename+"/deleteUser"}>Delete User</a></li>:null}
                <li className="divider"></li>
                {this.loggedIn ?<li><a href={this.props.basename+"/BMInfo"}>Budget Manager</a></li>:null}
                <li className="divider"></li>
                {this.loggedIn ?<li><a href={this.props.basename+"/SavingsManagerInformations"}>Saving Manager</a></li>:null}
                <li className="divider"></li>
                {this.loggedIn ?<li><a href={this.props.basename+"/ExpensesManagerInformations"}>Expenses Manager</a></li>:null}
                {this.loggedIn ?<li><a href={this.props.basename+"/ExpensesManagerInformations/add"}>Add Limit</a></li>:null}
                <li className="divider"></li>
                {this.loggedIn ?<li><a href={this.props.basename+"/Challenges"}>Challenges</a></li>:null}
                <li><a href={this.props.basename+"/Leaderboard"}>Leader board</a></li>
                <li><a href={this.props.basename+"/"}>About Us</a></li>
            </ul>
            <nav className = "pink">
                <div className="nav-wrapper">
                    <a href="/" className="brand-logo">Smart Saver</a>
                    <ul className="right hide-on-med-and-down">
                        <li><a className="dropdown-trigger" href="#!" onClick={this.toggleNavbar} data-target="dropdown">Menu</a></li>
                    </ul>
                </div>
            </nav>
        </div>
    );
  }
}
