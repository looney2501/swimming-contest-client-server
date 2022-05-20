import React from 'react';
import './RaceApp.css'

class RaceRow extends React.Component {

    handleDelete = () => {
        console.log('delete button pentru ' + this.props.race.id);
        this.props.deleteFunc(this.props.race.id);
    }

    render() {
        return (<tr>
                <td>{this.props.race.id}</td>
                <td>{this.props.race.distance}</td>
                <td>{this.props.race.style}</td>
                <td>{this.props.race.swimmersNumber}</td>
                <td>
                    <button onClick={this.handleDelete}>Delete</button>
                </td>
            </tr>);
    }
}

class RaceTable extends React.Component {
    render() {
        let rows = [];
        let functieStergere = this.props.deleteFunc;
        let functieModificare = this.props.modifyFunc;
        this.props.races.forEach(function (race) {
            rows.push(<RaceRow race={race} key={race.id} deleteFunc={functieStergere} modifyFunc={functieModificare}/>);
        });
        return (<div className="RaceTable">

                <table className="center">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Distance</th>
                        <th>Style</th>
                        <th>SwimmersNumber</th>

                        <th>Actions</th>
                    </tr>
                    </thead>
                    <tbody>{rows}</tbody>
                </table>

            </div>);
    }
}

export default RaceTable;