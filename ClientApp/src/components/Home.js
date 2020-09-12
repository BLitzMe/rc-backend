import React, { Component } from "react";
import DeviceOverview from "./DeviceOverview";

export class Home extends Component {
    static displayName = Home.name;

    render () {
        return (
            <div>
                <h1>Reparaturkonsole</h1>
                <DeviceOverview />
            </div>
        );
    }
}
