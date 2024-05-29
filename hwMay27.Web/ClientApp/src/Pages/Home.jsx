import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';
import axios from 'axios';

const Home = () => {

    const [people, setPeople] = useState([]);

    const getPeople = async () => {
        const { data } = await axios.get('/api/person/getpeople');
        setPeople(data);
    }

    useEffect(() => {
        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('/api/person/deleteall');
        getPeople();
    }

    return (<div>
        <button onClick={onDeleteClick} className='btn btn-danger'>Delete All</button>
        <table className='table table-bordered table-hover'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Age</th>
                    <th>Address</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {people.map(p =>
                    <tr>
                        <td>{p.id}</td>
                        <td>{p.firstName}</td>
                        <td>{p.lastName}</td>
                        <td>{p.age}</td>
                        <td>{p.address}</td>
                        <td>{p.email}</td>
                    </tr>
                )}
            </tbody>
        </table>
    </div>)
};

export default Home;