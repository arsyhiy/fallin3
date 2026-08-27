extends CharacterBody3D

@export var target: Node3D
@export var speed := 3.0

@onready var navigation_agent: NavigationAgent3D = $NavigationAgent3D


func _physics_process(_delta):
	if target == null:
		return

	navigation_agent.target_position = target.global_position

	if navigation_agent.is_navigation_finished():
		velocity = Vector3.ZERO
		move_and_slide()
		return

	var next_position := navigation_agent.get_next_path_position()
	var direction := global_position.direction_to(next_position)

	print(
		"enemy=", global_position,
		" next=", next_position,
		" direction=", direction
	)

	velocity = Vector3(
		direction.x * speed,
		0.0,
		direction.z * speed
	)

	move_and_slide()
