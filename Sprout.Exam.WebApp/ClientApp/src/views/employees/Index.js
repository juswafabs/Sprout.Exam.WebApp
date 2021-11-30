import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeesIndex extends Component {
  static displayName = EmployeesIndex.name;

  constructor(props) {
    super(props);
    this.state = { employees: [], loading: true };
  }

  componentDidMount() {
    this.populateEmployeeData();
  }

  static renderEmployeesTable(employees,parent) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Full Name</th>
            <th>Birthdate</th>
            <th>TIN</th>
            <th>Type</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(employee =>
            <tr key={employee.id}>
              <td>{employee.fullName}</td>
              <td>{parent.formatDate(employee.birthdate)}</td>
              <td>{employee.tin}</td>
              <td>{employee.employeeTypeId === 1?"Regular":"Contractual"}</td>
              <td>
              <button type='button' className='btn btn-info mr-2' onClick={() => parent.props.history.push("/employees/" + employee.id + "/edit")} >Edit</button>
              <button type='button' className='btn btn-primary mr-2' onClick={() => parent.props.history.push("/employees/" + employee.id + "/calculate")}>Calculate</button>
            <button type='button' className='btn btn-danger mr-2' onClick={() => {
              if (window.confirm("Are you sure you want to delete?")) {
                parent.deleteEmployee(employee.id);
              } 
            } }>Delete</button></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : EmployeesIndex.renderEmployeesTable(this.state.employees,this);

    return (
      <div>
        <h1 id="tabelLabel" >Employees</h1>
        <p>This page should fetch data from the server.</p>
        <p><button type='button' className='btn btn-success mr-2' onClick={() => this.props.history.push("/employees/create")} >Create</button></p>
        {contents}
      </div>
    );
  }

  formatDate(data) {
    return new Date(data).toLocaleDateString();
  }

  async populateEmployeeData() {

    try {
      const token = await authService.getAccessToken();
      const response = await fetch('api/employees', {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
      });
  
      if (response.status == 401)
        this.props.history.push("/authentication/login");
  
      const employees = await response.json();
      this.setState({ employees: employees.data, loading: false });
    } catch (error) {
        this.setState({ loading: false });
        alert("An error has occured. Please contact support.")
    }

  }

  async deleteEmployee(id) {

    try {
        const token = await authService.getAccessToken();
        const requestOptions = {
            method: 'DELETE',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' }
        };
        const response = await fetch('api/employees/' + id,requestOptions);
        const data = await response.json();
    
        if(data.isSuccessful){
            alert("Employee successfully deleted");
            this.populateEmployeeData();
        }
        else{
            alert(data.error);
        }
    } catch (error) {
        alert("An error has occured. Please contact support.")
    }
  }
}
