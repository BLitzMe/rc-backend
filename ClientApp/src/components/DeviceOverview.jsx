import React, { Component } from "react";
import Select from "react-select";

const defaultProps = {
    devices: [],
    options: [],
    selected: "",
    currentDevice: null
}

class DeviceOverview extends Component {
    static defaultProps = defaultProps;

    constructor(props = defaultProps) {
        super(props);
        this.state = defaultProps;
        this.selected = this.selected.bind(this);
    }

    async componentDidMount() {
        fetch("api/devices")
        .then(res => res.json())
            .then(data => this.setState({ devices: data }))
        .then(() => {
            let options = [];
            this.state.devices.forEach(device => {
                console.log(device);
                options.push({ value: device.id, label: device.model });
            });
            this.setState({ options: options });
        });
    }

    selected(selected) {
        let device = this.state.devices.filter(dev => {
            return dev.formID === selected.value;
        })[0];

        this.setState({ selected: selected });
        this.setState({ currentDevice: device });
    }

    render() {
        const postItems = this.state.devices.map(post => (
            <div key={post.formID}>
                <h3>{post.deviceModel}</h3>
                <p>{post.deviceDescription}</p>
            </div>
        ));

        return (
            <div>
                <h1>Gerät wählen</h1>
                <Select options={this.state.options} onChange={this.selected} />
                <p>{JSON.stringify(this.state.currentDevice, null, 2)}</p>
            </div>
        );
    }
}

export default DeviceOverview;