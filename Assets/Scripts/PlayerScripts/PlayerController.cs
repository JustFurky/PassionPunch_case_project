using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using HashTable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public int Score;
    [Header("Speed Variables")]
    [SerializeField] float _mouseSensivity, _movementSpeed, _runSpeed, _jumpHeight;
    [Header("Player Components")]
    [SerializeField] GunManager _playerGunManager;
    [SerializeField] PhotonView _characterPhotonView;
    [SerializeField] CharacterController _playerCharacterController;
    [SerializeField] GameObject _cameraHolderObject;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundMask;

    //privateVeriables
    private float _verticalLookRotation;
    float _gravity = -9.81f;
    Vector3 _velocityY;
    bool _isGrounded;
    private void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
        if (!_characterPhotonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (!_characterPhotonView.IsMine&&!PhotonNetwork.IsMasterClient)
            return;
        Look();
        Move();
        Jump();
        GroundedCheckAndVelocityFunction();
    }
    // can be: we can write more optimized InputManager script for players. This is basic FPS Controller.
    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _mouseSensivity);
        _verticalLookRotation += Input.GetAxisRaw("Mouse Y") * _mouseSensivity;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);
        _cameraHolderObject.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 characterMovementDirection = transform.right * x + transform.forward * z;
        _playerCharacterController.Move(characterMovementDirection * _movementSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocityY.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
    }
    private void GroundedCheckAndVelocityFunction()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, .5f, _groundMask);
        if (_isGrounded && _velocityY.y < 0)
        {
            _velocityY.y = -2f;
        }
        _velocityY.y += _gravity * Time.deltaTime;
        _playerCharacterController.Move(_velocityY * Time.deltaTime);
    }

    public void SetScore()
    {
        _characterPhotonView.RPC("setScore",RpcTarget.All);
    }
    [PunRPC]
    private void setScore()
    {
        HashTable hash = new HashTable();
        hash.Add("score", Score);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
