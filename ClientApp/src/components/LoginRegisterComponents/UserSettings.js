import React, { Component } from "react";
import {URL} from "../../Secrets";

var token = "";

export default class UserSettings extends Component {
  onChangeUsername(e) {
    e.preventDefault();

    const newUser = {
      username: this.refs.username.value,
    };

    console.log(newUser)
    this.updateUser(newUser)
  }
  onChangePassword(e) {
    e.preventDefault();
    
    const newUser = {
      password: this.refs.password.value,
      confPassword: this.refs.confirmedPassword.value
    };

    
    if(newUser.password!=newUser.confPassword){
      alert("Passwords dont match")
      return
    }
    
    if (!newUser.password.match(/^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]).*$/)) {
      alert("Please enter strong and secure password")
      return
    }
  
    console.log(newUser)
    this.updateUser(newUser)
  }

  updateUser(newUser){
    fetch(URL+"/api/UserInformations", {
      method: 'PUT',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
      },
      body: JSON.stringify({
          Username: newUser.username,
          Password: newUser.password
      })
    }).then(response => {
      console.log(response)
        })    

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
