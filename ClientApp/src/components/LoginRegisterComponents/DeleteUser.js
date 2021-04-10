import React, { Component } from "react";


export default class DeleteUser extends Component {

  deleteUser() {
    alert("User will be deleted");
    fetch("https://localhost:5001/api/UserInformations", {
        method: "DELETE",
        headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
        },
    }).then(response => {
        console.log(response);
        if (response.ok) {
            alert("Request returned ok");
        } else {
            alert("Request failed");
        }
    });
  }


  render() {
    return (
      <div>
        <h3>You are about to delete your account. Are you sure you want to do this?</h3>

        <button type="submit" className="btn btn-primary btn-block" onClick={this.deleteUser}>
          Yes
        </button>
      </div>
    );
  }
}
