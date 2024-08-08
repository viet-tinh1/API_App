import React from 'react';
import { useEffect, useState } from 'react';
import './App.css';
function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Email</th>
                    <th>Companyname</th>
                    <th>City </th>
                    <th>Country</th>
                    <th>Password</th>
                    <th>Order</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.memberId}>
                        <td>{forecast.email}</td>
                        <td>{forecast.city}</td>
                        <td>{forecast.country}</td>
                        <td>{forecast.password}</td>
                        <td>{forecast.orders}</td>
                        <a href='index.html'>Edit</a>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Member</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('/api/member/getMember');
        const data = await response.json();
        setForecasts(data);
    }
}



export default App;