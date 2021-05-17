import React, { Component } from "react";
import {URL} from "./../../Secrets";

var token = "";

export default class Login extends Component {
  
  constructor(props) {
    super(props)
    this.state = {email: ""};
    this.handleEmailChange=this.handleEmailChange.bind(this)
    this.onForgotPassword=this.onForgotPassword.bind(this)
  }
  
  handleEmailChange(e) {
    this.setState({email: e.target.value});
  }
  
  onForgotPassword(e){
    e.preventDefault()
    if(this.state.email===""){
      alert("Please enter email that you want to reset")
      return
    }


    fetch(URL+"/api/UserInformations/email", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(this.state.email)
    }).then(response => {
      if (response.ok) {
        alert("Email was sent to "+this.state.email)
      } else {
        alert("Failed to send email. Something went wrong. Please check your input and try again");
      }
    });
  }

  onSubmit(e) {

    const newUser = {
      email: this.refs.email.value,
      password: this.refs.password.value
    };

    this.getUser(newUser);
    e.preventDefault();
  }

  getUser(newUser) {
    fetch(URL+"/api/Login", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        Password: newUser.password,
        Email: newUser.email
      })
    }).then(response => {
      console.log(response);

      if (response.ok) {
        response.json().then(data => {
          document.cookie="token="+data.token; 
          this.props.history.push("/SavingsManagerInformations");
        })
      } else {
        console.log(response.ok);
        alert("Failed to login, try signing up");
        this.props.history.push("/log-in");
      }
    });
  }


  render() {
    return (
      <form onSubmit={this.onSubmit.bind(this)} >
        <h3>Log In</h3>

        <div className="imput-field">
          <label>Email address</label>
          <input
            type="email"
            name="email"
            ref="email"
            className="form-control"
            placeholder="Enter email"
            value={this.state.email}
            onChange={this.handleEmailChange}
          />
        </div>

        <div className="imput-field">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            name="password"
            ref="password"
            placeholder="Enter password"
          />
        </div>

        <div className="form-group">
          <div className="custom-control custom-checkbox">
            <input
              type="checkbox"
              className="custom-control-input"
              id="customCheck1"
            />
            <label className="custom-control-label" htmlFor="customCheck1">
              Remember me
            </label>
          </div>
        </div>

        <button type="submit" className="btn btn-primary btn-block">
          Submit
        </button>
        <p className="forgot-password text-right">
          Forgot <a href="#" onClick={this.onForgotPassword}>password?</a>
        </p>
      </form>
    );
  }
}
