import React, { Component } from "react";
import {URL} from "../../Secrets";
import "./delUser.css"

let styles={
    h3Style:{
        margin:20
    },
    divStyle:{
        textAlign:"center"
    }

}

export default class DeleteUser extends Component {

    deleteUser =()=> {
        alert("User will be deleted");
        fetch(URL+"/api/UserInformations", {method: "DELETE"}).then(response=>{
            response.json().then(data=>{
                if(data.success){
                    document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                    alert("Request returned ok");
                    this.props.history.push("/log-in");
                }else {
                    alert("Request failed");
                }
            })
        })
    }

    render() {
        return (
        <div style={styles.divStyle}>
            <h3 style={styles.h3Style}>You are about to delete your account. Are you sure you want to do this?</h3>
            <button type="submit" className="btn btn-primary delButton" onClick={this.deleteUser}>
            Yes
            </button>
        </div>
        );
    }
}
