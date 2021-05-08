import React, { Component } from "react";
import {URL} from "../../Secrets";

var token = "";

export default class UserSettings extends Component {
  onChangeUsername(e) {

    const newUser = {
      username: this.refs.username.value,
    };

    console.log(newUser)
    e.preventDefault();
  }
  onChangePassword(e) {

    const newUser = {
      password: this.refs.password.value,
      confPassword: this.refs.confirmedPassword.value
    };

    console.log(newUser)
    e.preventDefault();
  }

  render() {
    return (
    <div>
    <h3>User settings</h3>
    <div style={{display:"flex", justifyContent:"center"}}>
      <form onSubmit={this.onChangeUsername.bind(this)}  style={{display: "flex", flexDirection:"column", margin:"0 auto"}}>

        <div className="imput-field">
          <label>Change Username</label>
          <input
            type="text"
            name="username"
            ref="username"
            className="form-control"
            placeholder="Enter New Username"
            />
        </div>

        <button type="submit" className="btn btn-primary btn-block" style={{marginTop:"auto"}}>
          Change Username
        </button>
        </form>

        <form onSubmit={this.onChangePassword.bind(this)} style={{margin:"0 auto"}}>
        <div className="imput-field">
          <label htmlFor="password">Change Password</label>
          <input
            type="password"
            name="password"
            ref="password"
            placeholder="Enter New Password"
            />
        </div>

        <div className="imput-field">
          <label htmlFor="password">Confirm New Password</label>
          <input
            type="password"
            name="confirmedPassword"
            ref="confirmedPassword"
            placeholder="Confirm New Password"
          />
        </div>

        <button type="submit" className="btn btn-primary btn-block">
          Change Password
        </button>
      </form>
    </div>
    </div>
    );
  }
}
